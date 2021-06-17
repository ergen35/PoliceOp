using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoliceOp.API.Services
{
    public class JWTServices
    {
        private readonly string _secret;

        public JWTServices(IConfiguration configuration)
        {
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
                                  .AddClaim(ClaimName.ExpirationTime, DateTimeOffset.UtcNow.AddMinutes(240).ToUnixTimeSeconds())
                                  //.AddClaim(ClaimName.Issuer, "API")
                                  //.AddClaim(ClaimName.Subject, "SessionID")
                                  //.AddClaim(ClaimName.Audience, "http://localhost")

                                  .AddClaim("DataObject", DataObject)  //Store a whole object 
                                  .Encode();


            return token;
        }

        //Terminal -> API - TokenizeID with reason
        public string TokenizeID(string Matricule, string Password, string Reason, double expIMinutes = 5)
        {
            var token = JwtBuilder.Create()

                                  .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric

                                  .WithSecret(_secret)

                                  .AddClaim(ClaimName.ExpirationTime, DateTimeOffset.UtcNow.AddMinutes(expIMinutes).ToUnixTimeSeconds())

                                  .AddClaim("mat", Matricule)
                                  .AddClaim("pwd", Password)
                                  .AddClaim("bks", Reason)

                                  .Encode();


            return token;
        }


        //Terminal -> API - TokenizeID with reason
        public string TokenizeID(string Matricule, string Password, string Reason, Models.Issuers Issuer, Models.Audiences Audience, double expIMinutes = 5)
        {
            var token = JwtBuilder.Create()

                                  .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric

                                  .WithSecret(_secret)

                                  .AddClaim(ClaimName.ExpirationTime, DateTimeOffset.UtcNow.AddMinutes(expIMinutes).ToUnixTimeSeconds())

                                  .AddClaim(ClaimName.Issuer, Issuer.ToString())
                                  .AddClaim(ClaimName.Audience, Audience.ToString())

                                  .AddClaim("mat", Matricule)
                                  .AddClaim("pwd", Password)
                                  .AddClaim("bks", Reason)

                                  .Encode();


            return token;
        }

        //Terminal -> API - Tokenize SessionID with a reason
        public string TokenizeSessionID(string SessionID, string Reason, double expIMinutes = 20)
        {
            var token = JwtBuilder.Create()

                                  .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric

                                  .WithSecret(_secret)

                                  .AddClaim(ClaimName.ExpirationTime, DateTimeOffset.UtcNow.AddMinutes(expIMinutes).ToUnixTimeSeconds())
                                  .AddClaim("sid", SessionID)
                                  .AddClaim("bks", Reason)

                                  .Encode();


            return token;
        }

        public IDictionary<string, string> DecodeObjectFromToken(string token)
        {
            try
            {
                var payload = JwtBuilder.Create()
                        .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                        .WithSecret(_secret)
                        .MustVerifySignature()
                        .Decode<IDictionary<string, string>>(token);

                return payload;
            }
            catch (Exception ex) when (ex is JWT.Exceptions.InvalidTokenPartsException || ex is JWT.Exceptions.SignatureVerificationException || ex is JWT.Exceptions.TokenExpiredException)
            {
                return null;
            }

        }

    }
}
