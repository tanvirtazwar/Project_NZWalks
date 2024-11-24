using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO
{
    public class ImageUpdateRequestDto
    {
        [Required]
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
