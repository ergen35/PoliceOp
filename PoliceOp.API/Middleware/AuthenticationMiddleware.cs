using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


namespace PoliceOp.API.Middleware
{
    public static class AuthenticationMiddleware
    {
        public static IServiceCollection AddJwtTokenAuthentification(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("JwtConfig").GetSection("secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication( Options =>
           {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           
            .AddJwtBearer( Options =>

           {
               Options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuerSigningKey = true,
                   ValidateLifetime = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = true,
                   ValidateAudience = true,
               };

           });

            return services;
        }
    }
}
