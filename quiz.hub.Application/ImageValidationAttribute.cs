using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace quiz.hub.Application
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        private readonly string _maxSizeInMB;
        private readonly string _allowedExtentions;
        public ImageValidationAttribute(string maxSizeInMB, string allowedExtentions)
        {
            _maxSizeInMB = maxSizeInMB;
            _allowedExtentions = allowedExtentions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var imageFile = value as IFormFile;

            if (imageFile != null)
            {
                if (imageFile.Length > Convert.ToInt32(_maxSizeInMB) * 1048576 ||
                    !_allowedExtentions.Contains(Path.GetExtension(imageFile.FileName).ToLower()))
                {
                    return new ValidationResult($"Only accept Image with extentions {_allowedExtentions} and max size of {_maxSizeInMB} MB");
                }
            }

            return ValidationResult.Success;
        }
    }
}
