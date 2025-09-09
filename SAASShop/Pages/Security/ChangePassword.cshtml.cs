using Microsoft.AspNetCore.Mvc;
using SAASShop.Pages;
using SAASShopDataAccess;
using SAASShopDomain;
using SAASShopDomain.User;
using System.Security.Claims;

namespace SAASShop.Pages.Security
{
    public class ChangePasswordModel : TransactBasePage
    {


        private readonly ISecurity m_Security;
        private readonly IDbLogger m_Log;
        private readonly ISysConfig m_Config;

        public ChangePasswordModel(ISecurity securityMngr, ISysConfig sysConfig, IDbLogger logger)
        {
            m_Security = securityMngr;
            m_Config = sysConfig;
            m_Log = logger;
        }

        [BindProperty]
        public Models.ChangePasswordModel ChangePasswordModelData { get; set; }

        public void OnGet()
        {
            try
            {
                SetCCInfo(Navigator.Login);
            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
                throw;
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid != true)
                {
                    return Page();
                }

                var systemConfiguration =  m_Config.GetSystemConfiguration(ConfigurationCode.PasswordLength);

                if (User.Claims.Any())
                {
                    if (User.Claims.FirstOrDefault(s => s.Type == System.Security.Claims.ClaimTypes.Name).Value.ToUpper() != ChangePasswordModelData.LoginID.ToUpper())
                    {
                        return Page();
                    }
                }

                if (ChangePasswordModelData.NewPassword.Length < systemConfiguration.Value)
                {
                    ModelState.AddModelError(" ", $"Your New Password Length should not be less than {systemConfiguration.Value}");
                    return Page();
                }

                if (ChangePasswordModelData.NewPassword != ChangePasswordModelData.ConfirmPassword)
                {
                    ModelState.AddModelError(" ", $"New Password and Confirm Password Do Not Match");
                    return Page();
                }

                List<Claim> claims = User.Claims.ToList();

                var cl = HttpContext.Session.GetObjectFromJson<ClaimsIdentity>("claimsIdentity");

                if (claims.Count == 0 || claims is null)
                    claims = HttpContext.Session.GetObjectFromJson<List<Claim>>("TempClaims");

                bool hasChanged = m_Security.ChangePassword(ChangePasswordModelData.LoginID, ChangePasswordModelData.Password, ChangePasswordModelData.NewPassword);

                if (hasChanged)
                {
                    return RedirectToPage(Navigator.Dashboard);
                }
                else
                {
                    ModelState.AddModelError(" ", "Your Current Login ID & Password Do Not Match.  Please Try Again");
                    return Page();
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