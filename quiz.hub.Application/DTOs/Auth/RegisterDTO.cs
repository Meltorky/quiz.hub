using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;


        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Both passwords don't match !!")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
