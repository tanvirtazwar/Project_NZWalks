using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO;

public class AddRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Code has to be minimum of 3 character")]
    [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 character")]
    public string Code { get; set; } = null!;

    [Required]
    [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 character")]
    public string Name { get; set; } = null!;

    public string? RegionImageUrl { get; set; }
}
