using Utility = SAASShop.Pages.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SAASShop.Pages;
using SAASShop.Communications;
using SAASShopDataAccess;
using SAASShopDomain.User;
using SAASShopDomain;

namespace Hemaiya.Communications
{
    public class AuthServiceManager : IAuthService
    {
        private readonly ISecurity m_UserSecurity;
        public AuthServiceManager(ISecurity security)
        {
            m_UserSecurity = security;
        }

        public async Task<List<Claim>> SetupAuthClaims(AppUser user, HttpContext context)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.IsPersistent, "false", "bool"),
                        new Claim(ClaimTypes.Name, user.LoginId),
                        new Claim(ClaimTypes.GivenName, user.FullName),
                        new Claim("UserId", user.Id.ToString()),
                    };


            //Add Primary Access
            string PrimaryModule = ((Roles)(user.AppUserRoles.FirstOrDefault().AppRoleId)).GetEnumCategory();
            claims.Add(new Claim(PrimaryModule + "_HasAccess", "true", "bool"));

            foreach (var role in user.AppUserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.AppRole.Role));
                claims.Add(new Claim("RoleId", role.AppRole.Id.ToString()));

               
            }
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return claims;
        }

        //public async Task AuthProcess(List<Claim> claims, HttpContext context)
        //{
        //    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        //    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);                        
        //}
    }
}
