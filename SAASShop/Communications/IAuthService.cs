using SAASShopDomain.User;
using System.Security.Claims;

namespace SAASShop.Communications
{
    public interface IAuthService
    {
        Task<List<Claim>> SetupAuthClaims(AppUser user, HttpContext context);
    }
}
