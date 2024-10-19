using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GM_API.Controllers.Authorization
{
    public class JwtController
    {

        public static string GenerateJwt(string user)
        {
            var key = "1uCpfKVEM7F7PnMJ1ZQ5SlduRbf8osymdwaSDkOPWoW=";
            var tokenHandler = new JwtSecurityTokenHandler();

            var byteKey = Encoding.UTF8.GetBytes(key);
            var tokenDes = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
                
            };
            var token = tokenHandler.CreateToken(tokenDes);

            return tokenHandler.WriteToken(token);
        }
    }
}
