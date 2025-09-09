using CommonLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShop.Communications;
using SAASShopDataAccess;
using SAASShopDomain;
using SAASShopDomain.User;
using System.Security.Claims;

namespace SAASShop.Pages.Security
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public SAASShop.Models.LoginModel UserLogin { get; set; }

        private List<Claim> UserClaims { get; set; }

        private readonly ISecurity m_UserSecurity;
        private readonly IDbLogger m_Log;
        private readonly ISysConfig m_SysConfig;
        private readonly IAuthService m_AuthService;

        public LoginModel(ISecurity security, IDbLogger logger, ISysConfig sysConfig, IAuthService authService)
        {
            m_UserSecurity = security;
            m_Log = logger;
            m_SysConfig = sysConfig;
            m_AuthService = authService;
        }

        public async Task<IActionResult> OnGetLogOff()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToPage(Navigator.Login);
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                AppUser user = m_UserSecurity.AuthenticateUserCredentials(UserLogin.LoginID, UserLogin.Password);

                if (user == null)
                {
                    TempData["Message"] = "Invalid login attempt. Please try again.";
                    return RedirectToPage(Navigator.Login);
                }

                HttpContext.Session.SetString("AppUserActivityID", user.Id.ToString());
                HttpContext.Session.SetString("LastActiveOn", TimeZoneUtility.DateTimeNow.ToString());

                //if (user.AppUserRoles.Count == 0)
                //{
                //    TempData["Message"] = "User is not assigned a Role. Please contact your IT";
                //    return RedirectToPage(Navigator.Login);
                //}

                var shouldEnforce = m_SysConfig.GetSystemConfiguration(ConfigurationCode.EnforceMFA);

                if (shouldEnforce is not null && shouldEnforce.IsActive)
                {
                    HttpContext.Session.SetString("LoginID", UserLogin.LoginID);
                    HttpContext.Session.SetString("Password", UserLogin.Password);

                    return RedirectToPage(Navigator.MFAOption);
                }

                UserClaims = await m_AuthService.SetupAuthClaims(user, this.HttpContext);

                return RedirectToPage(Navigator.Dashboard);

            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
                throw;
            }
        }


    }
}