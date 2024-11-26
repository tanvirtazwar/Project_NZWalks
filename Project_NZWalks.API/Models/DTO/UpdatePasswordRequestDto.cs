using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO;

public class UpdatePasswordRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;
}