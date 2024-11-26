using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO;

public class ImageUploadRequestDto
{
    [Required] 
    public IFormFile File { get; set; } = null!;

    [Required]
    public string FileName { get; set; } = null!;

    public string? FileDescription { get; set; }
}
