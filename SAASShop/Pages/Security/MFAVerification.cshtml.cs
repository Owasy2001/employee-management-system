using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShop.Communications;
using SAASShopDataAccess;
using SAASShopDomain.User;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace SAASShop.Pages.Security
{
    public class MFAVerificationModel : PageModel
    {
        private List<Claim> UserClaims { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Verification Code is required")]
        public string SecurityCode { get; set; }

        private readonly IAuthService m_AuthService;
        private readonly ISecurity m_UserSecurity;
        private readonly IDbLogger m_Log;

        public MFAVerificationModel(ISecurity security, IDbLogger logger, IAuthService authService)
        {
            m_UserSecurity = security;
            m_Log = logger;
            m_AuthService = authService;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (!string.IsNullOrEmpty(SecurityCode) && HttpContext.Session.GetString("SecurityCode") == SecurityCode)
                {
                    if (Convert.ToBoolean(HttpContext.Session.GetString("IsPasswordReset")))
                    {
                        return RedirectToPage(Navigator.ResetNewPassword);
                    }
                    else
                    {
                        AppUser user = m_UserSecurity.AuthenticateUserCredentials(HttpContext.Session.GetString("LoginID"), HttpContext.Session.GetString("Password"));

                        UserClaims = await m_AuthService.SetupAuthClaims(user, this.HttpContext);
                        return RedirectToPage(Navigator.Dashboard);
                    }
                }
                else
                {
                    TempData["Message"] = "Please enter the correct Verification Code.";
                    return RedirectToPage(Navigator.VerifyMFA);
                }
            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
                throw;
            }
        }
    }
}

