using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO;

public class UpdatePasswordRequestDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
}