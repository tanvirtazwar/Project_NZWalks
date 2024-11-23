using Microsoft.AspNetCore.Mvc;
using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Models.DTO;
using Project_NZWalks.API.Repositories;

namespace Project_NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController(IImageRepository imageRepository) : ControllerBase
{
    //Post: api/Images/Upload
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
    {
        ValidateFileUpload(imageUploadRequestDto);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Convert Dto to Domain Model
        var imageDomainModel = new Image
        {
            File = imageUploadRequestDto.File,
            FileName = imageUploadRequestDto.FileName,
            FileDescription = imageUploadRequestDto.FileDescription,
            FileExtensions = Path.GetExtension(imageUploadRequestDto.File.FileName),
            FileSizeInBytes = imageUploadRequestDto.File.Length
        };

        //Use Repository to Upload Image
        await imageRepository.UploadAsync(imageDomainModel);

        return Ok(imageDomainModel);
    }

    private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", "png" };

        if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
        {
            ModelState.AddModelError("file", "Unsupported file extension");
        }

        if(imageUploadRequestDto.File.Length > 10485760)
        {
            ModelState.AddModelError("file","File size more than 10MB");
        }
    }
}
