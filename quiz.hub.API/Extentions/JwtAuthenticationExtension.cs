using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using quiz.hub.Application.Options;
using System.Runtime.CompilerServices;
using System.Text;

namespace quiz.hub.API.Extentions
{
    public static class JwtAuthenticationExtension
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services) 
        {
            services.AddAuthentication(opts => { opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;})
                .AddBearerToken();

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<JwtOptions>>((options, jwtOptionsAccessor) => 
                {
                    var jwtOptions = jwtOptionsAccessor.Value;

                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidIssuer = jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            return services;
        }
    }
}
