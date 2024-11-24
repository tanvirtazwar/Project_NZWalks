using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Data;
using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Models.DTO;

namespace Project_NZWalks.API.Repositories;

public class SQLLocalImageRepository
    (IWebHostEnvironment webHostEnvironment,
    IHttpContextAccessor httpContextAccessor, 
    NZWalksDbContext dbContext) 
    : IImageRepository
{
    public async Task<Image> UploadAsync(Image image)
    {
        var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,
            "Images", $"{image.FileName}{image.FileExtensions}");

        //Upload Image to Local File
        await using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        //Create URL path
        // http://localhost:1234/images/images.jpg
        var httpContext = httpContextAccessor.HttpContext!;
        var urlFilePath = $"{httpContext.Request.Scheme}" +
            $"://{httpContext.Request.Host}" +
            $"{httpContext.Request.PathBase}" +
            $"/Images/{image.FileName}{image.FileExtensions}";

        image.FilePath = urlFilePath;
        image.UserId = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        //Add Images to Image Table
        await dbContext.Images.AddAsync(image);
        await dbContext.SaveChangesAsync();
        return image;
    }


    public async Task<Image?> GetByIdAsync(Guid id)
    {
        return await dbContext.Images.FindAsync(id);
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        return await dbContext.Images.ToListAsync();
    }

    public async Task<Image?> UpdateAsync(Guid id, ImageUpdateRequestDto updateDto)
    {
        var image = await GetByIdAsync(id);
        if (image == null) return null;
        if (image.UserId != httpContextAccessor.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return null;
        }

        var filePath = Path.Combine(webHostEnvironment.ContentRootPath,
            "Images", $"{image.FileName}{image.FileExtensions}");

        if (!File.Exists(filePath))
        {
            return null;
        }

        var newFilePath = Path.Combine(webHostEnvironment.ContentRootPath,
            "Images", $"{updateDto.FileName}{image.FileExtensions}");

        File.Move(filePath, newFilePath);

        image.FileName = updateDto.FileName ?? image.FileName;
        image.FileDescription = updateDto.FileDescription ?? image.FileDescription;
        HttpContext httpContext = httpContextAccessor.HttpContext!;
        var urlFilePath = $"{httpContext.Request.Scheme}" +
            $"://{httpContext.Request.Host}" +
            $"{httpContext.Request.PathBase}" +
            $"/Images/{image.FileName}{image.FileExtensions}";

        image.FilePath = urlFilePath;

        dbContext.Images.Update(image);
        await dbContext.SaveChangesAsync();

        return image;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var image = await GetByIdAsync(id);
        if (image == null) return false;
        if (image.UserId != httpContextAccessor.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return false;
        }

        var filePath = Path.Combine(webHostEnvironment.ContentRootPath,
            "Images", $"{image.FileName}{image.FileExtensions}");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        dbContext.Images.Remove(image);
        await dbContext.SaveChangesAsync();

        return true;
    }
}
