using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RealTimeWebChat.Helpers
{
    public static class JwtHelper
    {
        public static void AddBearerAuth(this WebApplicationBuilder WebAppBuilder)
        {
            WebAppBuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = WebAppBuilder.Configuration["Jwt:Issuer"],
                        ValidAudience = WebAppBuilder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(WebAppBuilder.Configuration["Jwt:Key"]))
                    };
                }
                );
        }
    }
}
