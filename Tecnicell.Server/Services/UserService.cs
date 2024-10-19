using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tecnicell.Server.Context;
using Tecnicell.Server.Models.Common;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.Request;
using Tecnicell.Server.Models.Responses.Authorization;
using Tecnicell.Server.Tools;

namespace Tecnicell.Server.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserResponse Auth(TecnicellContext context, AuthRequest model)
        {
            UserResponse response = new UserResponse();
            using( var db = context )
            {
                string spassword = Encrypt.GetSHA256(model.Password);

                var usuario = db.UserAccounts.Where(user =>
                                user.Name == model.Name &&
                                user.Password == spassword).FirstOrDefault();

                if (usuario == null) throw new Exception();

                var usuarioInfo = db.UserInfos.Where(user => user.UserCode == usuario.UserCode).FirstOrDefault();

                response.Code = usuario.UserCode;
                response.Token = GetToken(usuarioInfo);
            };

            return response;
        }
        private string GetToken(UserInfo user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserCode),
                        new Claim(ClaimTypes.Role, user.Role)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
