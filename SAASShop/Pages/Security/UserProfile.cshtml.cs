using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain.User;

namespace SAASShop.Pages.Security
{
    public class UserProfileModel : PageModel
    {
        private readonly ISecurity m_Security;
        private readonly IDbLogger m_Log;

        public UserProfileModel(ISecurity securityMngr, IDbLogger logger)
        {
            m_Security = securityMngr;
            m_Log = logger;
        }

        [BindProperty]
        public AppUser ProfileData { get; set; }

        public void OnGet()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                ProfileData = m_Security.GetAppUser(userId);
            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
                throw;
            }
        }

        public void OnGetClearSignature()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                ProfileData = m_Security.GetAppUser(userId);
                m_Security.UpdateAppUser(ProfileData);
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

               
                if (!string.IsNullOrEmpty(ProfileData.Phone))
                {
                    ProfileData.Phone = ProfileData.Phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ","");
                }

                m_Security.UpdateAppUser(ProfileData);

                TempData["Message"] = "The Profile has been updated successfully";
                return RedirectToPage(Navigator.UserProfile);
            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
                throw;
            }
        }
    }
}