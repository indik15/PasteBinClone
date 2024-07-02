using PasteBinClone.Web.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace PasteBinClone.Web.Services
{
    public class UserInfo : IUserInfo
    {
        public string GetUserId(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                var handler = new JwtSecurityTokenHandler();

                var jwtToken = handler.ReadToken(accessToken) as JwtSecurityToken;

                var userId = jwtToken.Claims.FirstOrDefault(u => u.Type == "sub").Value;

                return userId;
            }

            return string.Empty;
        }
    }
}
