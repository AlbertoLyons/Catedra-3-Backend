using System.ComponentModel.DataAnnotations;

namespace Catedra_3_Backend.src.dtos
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}