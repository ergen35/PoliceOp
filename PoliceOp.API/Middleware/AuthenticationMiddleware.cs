using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PoliceOp.Models;
using System;
using System.Linq;
using System.Text;

namespace PoliceOp.API.Middleware
{


    public static class AuthenticationMiddleware
    {
        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("JwtConfig").GetSection("secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(Options =>
          {
              Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })

            .AddJwtBearer(Options =>

          {
              Options.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidateIssuerSigningKey = true,
                  ValidateLifetime = true,
                  IssuerSigningKey = new SymmetricSecurityKey(key),

                  ValidAudiences = Enum.GetNames(typeof(Audiences)).AsEnumerable(),
                  ValidIssuers = Enum.GetNames(typeof(Issuers)).AsEnumerable(),

                  ValidateIssuer = true,
                  ValidateAudience = true,
              };

          });

            return services;
        }
    }
}
