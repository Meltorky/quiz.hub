using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;

        [Range(0.1, 24, ErrorMessage = "Lifetime must be between 6 minutes and 24 Hours.")]
        public double LifeTimeInHours { get; set; }
    }
}
