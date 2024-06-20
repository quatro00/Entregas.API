using Microsoft.AspNetCore.Identity;

namespace Entregas.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
