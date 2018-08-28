using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharedKernel.Domain.Modelo;
using System;

namespace SharedKernel.Api.Security
{
    public static class ApiToken
    {
        public static string Secret { get; set; }

        public static void GerarSecret()
        {
            var secret = Guid.NewGuid().ToString("N").Substring(0, 20);
            Secret = secret;
        }

        public static string GerarTokenString(this Token token)
        {
            GerarSecret();

            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var tokenString = encoder.Encode(token, Secret);

            return tokenString;
        }

        public static string RecuperarTokenString(this HttpContext context)
        {
            return context.Request.Headers["Token"];
        }

        public static Token RecuperarToken(this HttpContext context)
        {
            try
            {
                var tokenString = context.RecuperarTokenString();

                var serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                var validator = new JwtValidator(serializer, provider);
                var urlEncoder = new JwtBase64UrlEncoder();
                var decoder = new JwtDecoder(serializer, validator, urlEncoder);
                
                var json = decoder.Decode(tokenString, Secret, verify: true);

                return JsonConvert.DeserializeObject<Token>(json);
            }
            catch (SignatureVerificationException)
            {
                Console.Out.WriteLine("Token Inválido!");
            }
            return null;
        }
    }
}