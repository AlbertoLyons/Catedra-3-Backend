using System.ComponentModel.DataAnnotations;

namespace Catedra_3_Backend.src.dtos.post
{
    public class CreatePostDTO
    {
        [Required]
        public string Title { get; set; } = "";
        
        [Required]
        public DateOnly PostDate { get; set; }
        
        [Required]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Image size cannot exceed 5MB.")]
        public IFormFile Image { get; set; } = null!;
        
        [Required]
        public string UserId { get; set; } = "";
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(ErrorMessage ?? $"File size cannot exceed {_maxFileSize} bytes.");
                }
            }
            return ValidationResult.Success!;
        }
    }
}