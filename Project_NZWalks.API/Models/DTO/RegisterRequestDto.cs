using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO;

public class RegisterRequestDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email {  get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!;
}
