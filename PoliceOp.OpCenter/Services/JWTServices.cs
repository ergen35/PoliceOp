using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;
using JWT;
using JWT.Serializers;

namespace PoliceOp.OpCenter.Services
{
    public class JWTServices
    {
        public string _secret { get; set; }
        
        public JWTServices()
        {
            this._secret = System.Configuration.ConfigurationManager.AppSettings["secret"].ToString();
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

                                  .AddClaim(ClaimName.Issuer, Models.Issuers.OpCenterApp.ToString())
                                  .AddClaim(ClaimName.Audience, Models.Audiences.PoliceOpAPI.ToString())

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
