using System.ComponentModel.DataAnnotations;
namespace Catedra_3_Backend.src.dtos
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; } = "";
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*\d).+$", ErrorMessage = "The password must contain at least one number.")]
        public string Password { get; set; } = "";
        [Required]
        public string ConfirmPassword { get; set; } = "";
    }
}