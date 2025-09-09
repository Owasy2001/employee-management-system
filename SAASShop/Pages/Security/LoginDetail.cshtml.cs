using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using System.ComponentModel.DataAnnotations;

namespace SAASShop.Pages.Security
{
    public class LoginDetailModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Login ID is required")]
        public string LoginID { get; set; }

        private readonly ISecurity m_UserSecurity;
        private readonly IDbLogger m_Log;

        public LoginDetailModel(ISecurity security, IDbLogger logger)
        {
            m_UserSecurity = security;
            m_Log = logger;

        }

        public IActionResult OnPost()
        {
            try
            {
                var User = m_UserSecurity.GetUserByLoginId(LoginID);

                if(User != null)
                {
                    HttpContext.Session.SetString("LoginID", LoginID);
                    HttpContext.Session.SetString("IsPasswordReset", "True");
                    return RedirectToPage(Navigator.MFAOption);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login ID");
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