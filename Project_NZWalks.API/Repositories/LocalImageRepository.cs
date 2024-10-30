using Project_NZWalks.API.Data;
using Project_NZWalks.API.Models.Domain;

namespace Project_NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NzWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, NzWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> UploadAsync(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Images", $"{image.FileName}{image.FileExtensions}");

            //Upload Image to Local File
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //Create URL path
            HttpContext? httpContext = httpContextAccessor.HttpContext!;
            var urlFilePath = $"{httpContext.Request.Scheme}" +
                $"://{httpContext.Request.Host}" +
                $"{httpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileExtensions}";

            image.FilePath = urlFilePath;

            //Add Images to Image Table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
