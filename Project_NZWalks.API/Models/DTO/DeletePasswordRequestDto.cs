using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO
{
    public class DeletePasswordRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
