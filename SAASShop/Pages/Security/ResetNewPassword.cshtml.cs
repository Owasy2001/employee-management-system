using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAASShop.Pages.Security
{
    public class ResetNewPasswordModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        private readonly ISecurity m_UserSecurity;
        private readonly IDbLogger m_Log;

        public ResetNewPasswordModel(ISecurity security, IDbLogger logger)
        {
            m_UserSecurity = security;
            m_Log = logger;

        }

        public IActionResult OnPost()
        {
            try
            {
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    var isPasswordReset = m_UserSecurity.ResetPassword(HttpContext.Session.GetString("LoginID"), NewPassword);

                    if (isPasswordReset)
                    {
                        HttpContext.Session.Clear();
                        return RedirectToPage(Navigator.Login);
                    }
                }

                return Page();

            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
                throw;
            }
        }
    }
}


