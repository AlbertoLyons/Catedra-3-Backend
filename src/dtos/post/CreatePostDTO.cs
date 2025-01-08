using System.ComponentModel.DataAnnotations;

namespace Catedra_3_Backend.src.dtos.post
{
    public class CreatePostDTO
    {
        [Required]
        public string Title { get; set; } = "";
        
        [Required]
        public DateTime PostDate { get; set; }
        
        [Required]
        public IFormFile Image { get; set; } = null!;
        
        [Required]
        public string UserId { get; set; } = "";
    }
}