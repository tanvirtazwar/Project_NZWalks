using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Models.DTO;
using Project_NZWalks.API.Repositories;

namespace Project_NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ImagesController(IImageRepository imageRepository) : ControllerBase
{
    [HttpPost]
    [Route("Upload")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
    {
        ValidateFileUpload(imageUploadRequestDto);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Convert DTO to Domain Model
            var imageDomainModel = new Image
            {
                File = imageUploadRequestDto.File,
                FileName = imageUploadRequestDto.FileName,
                FileDescription = imageUploadRequestDto.FileDescription,
                FileExtensions = Path.GetExtension(imageUploadRequestDto.File.FileName),
                FileSizeInBytes = imageUploadRequestDto.File.Length
            };

            // Upload Image
            var uploadedImage = await imageRepository.UploadAsync(imageDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = uploadedImage.Id }, uploadedImage);
        }
        catch (Exception ex)
        {
            // Log the error (add logging service here)
            return StatusCode(500, $"An error occurred while uploading the image. {ex.Message}");
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var image = await imageRepository.GetByIdAsync(id);
        if (image == null)
        {
            return NotFound("Image not found.");
        }

        return Ok(image);
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll()
    {
        var images = await imageRepository.GetAllAsync();
        return Ok(images);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ImageUpdateRequestDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedImage = await imageRepository.UpdateAsync(id, updateDto);
        if (updatedImage == null)
        {
            return NotFound("Image not found.");
        }

        return Ok(updatedImage);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isDeleted = await imageRepository.DeleteAsync(id);
        if (!isDeleted)
        {
            return NotFound("Image not found.");
        }

        return NoContent();
    }

    private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

        if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
        {
            ModelState.AddModelError("file", "Unsupported file extension");
        }

        if (imageUploadRequestDto.File.Length > 10485760) // 10MB limit
        {
            ModelState.AddModelError("file", "File size exceeds 10MB");
        }
    }
}
