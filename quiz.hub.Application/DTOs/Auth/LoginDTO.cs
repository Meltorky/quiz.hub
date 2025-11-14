using System.ComponentModel.DataAnnotations;

namespace quiz.hub.Application.DTOs.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        
        //[ImageValidation(ImageFileOptions.MaxSizeInMB,ImageFileOptions.AllowedExtentions)]
        //[SwaggerSchema(Description = ImageFileOptions.ErrorMessage)]
        //public IFormFile? cover { get; set; }
    }
}
