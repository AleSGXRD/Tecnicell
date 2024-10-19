using Tecnicell.Server.Context;
using Tecnicell.Server.Models.Request;
using Tecnicell.Server.Models.Responses.Authorization;

namespace Tecnicell.Server.Services
{
    public interface IUserService
    {
        UserResponse Auth(TecnicellContext context ,AuthRequest model);
    }
}
