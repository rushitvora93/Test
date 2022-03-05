using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace FrameworksAndDrivers.Authentication
{
    public class AuthenticationInformation
    {
        public readonly string Base64;
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public string PcFqdn { get; set; }

        public AuthenticationInformation(string base64)
        {
            Base64 = base64;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(base64) as JwtSecurityToken;
            if (token == null)
            {
                throw new ArgumentException("JWT Token not readable!");
            }

            var claimList = token.Claims.ToList();
            UserId = long.Parse(claimList.Find(x => x.Type == TokenProperties.UserId)?.Value);
            UserName = claimList.Find(x => x.Type == TokenProperties.UserName).Value;
            GroupId = long.Parse(claimList.Find(x => x.Type == TokenProperties.GroupId)?.Value);
            GroupName = claimList.Find(x => x.Type == TokenProperties.GroupName).Value;
            PcFqdn = claimList.Find(x => x.Type == TokenProperties.PcFqdn).Value;
        }
    }
}