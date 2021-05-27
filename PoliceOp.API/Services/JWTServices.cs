using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using JWT.Algorithms;
using JWT.Builder;

namespace PoliceOp.API.Services
{
    public class JWTServices
    {
        private readonly string _secret; 
        private readonly double _expTime;
        private readonly double _SessionSendexpTime;

        public JWTServices(IConfiguration configuration)
        {
            this._expTime = Convert.ToDouble(configuration.GetSection("JwtConfig").GetSection("expInMinutes").Value);
            this._SessionSendexpTime = Convert.ToDouble(configuration.GetSection("JwtConfig").GetSection("SessionSendexpInMinutes").Value);
            this._secret = configuration.GetSection("JwtConfig").GetSection("secret").Value;
        }

        //Using JWT.NET Package
        public string GetTokenFromRequest(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            return token;  //Token can be null, so it's to check if null
        }

        public string GenerateTokenFromObject<T>(T DataObject)
        {
            var token = JwtBuilder.Create()
              .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
              .WithSecret(_secret)
              .AddClaim(ClaimName.Issuer, "PoliceOP.API")
              .AddClaim(ClaimName.Subject, "SessionID")
              .AddClaim(ClaimName.Audience, "PoliceOp.Terminal")
              .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(240).ToUnixTimeSeconds())
              .AddClaim("DataObject", DataObject)  //Store a whole object 
              .Encode();
            

            return token;
        }

        public T DecodeObjectFromToken<T>(string token) where T: class
        {
            try
            {
                var payload = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                .WithSecret(_secret)
                .MustVerifySignature()
                .Decode<T>(token);

                 return payload;
            }
            catch (Exception ex) when (ex is JWT.Exceptions.InvalidTokenPartsException || ex is JWT.Exceptions.SignatureVerificationException || ex is JWT.Exceptions.TokenExpiredException)
            {
                return null;
            }

        }

    }
}
