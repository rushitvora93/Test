using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.UseCases.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FrameworksAndDrivers.Authentication
{
    public class JWTBearerGenerator : ITokenGenerator
    {
        private SecurityKey key_;
        public JWTBearerGenerator(SecurityKey key)
        {
            key_ = key;
        }

        public string GenerateToken(AuthenticationUser user, string pcfqdn)
        {
            var qstIssuer = "QSTV8-CORE";
            var qstAudience = "QSTV8-Clients";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(TokenProperties.UserId,user.UserId.ToString()),
                        new Claim(TokenProperties.UserName,user.UserName),
                        new Claim(TokenProperties.GroupId,user.GroupId.ToString()),
                        new Claim(TokenProperties.GroupName,user.GroupName),
                        new Claim(TokenProperties.PcFqdn, pcfqdn)
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = qstIssuer,
                Audience = qstAudience,
                SigningCredentials = new SigningCredentials(key_, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
