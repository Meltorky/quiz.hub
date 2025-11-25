using quiz.hub.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace quiz.hub.Application.DTOs.QuizDTOs
{
    public class GetAllDTO
    {
        [Required]
        public string userId { get; set; } = default!;   

        [Required]
        [SwaggerSchema(Description = "Select one [ Admin / Host / Candidate ]")]
        public string Position { get; set; } = string.Empty;

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
