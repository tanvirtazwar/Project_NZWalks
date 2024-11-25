using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO
{
    public class DeletePasswordRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
