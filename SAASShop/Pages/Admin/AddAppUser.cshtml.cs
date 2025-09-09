using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;
using SAASShopDomain.User;

namespace SAASShop.Pages.Admin
{
	public class AddAppUserModel : PageModel
	{
		[BindProperty]
		public AppUser AppUserData { get; set; }

        private readonly ISecurity m_UserSecurity;
		private readonly IDbLogger m_Log;

		public AddAppUserModel(ISecurity security, IDbLogger logger)
		{
			m_UserSecurity = security;
			m_Log = logger;
		}

		public void OnGet()
		{
            try
            {
                AppUserData = new AppUser();
            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
                throw;
            }
        }

		public IActionResult OnPost()
		{
			if (ModelState.IsValid != true)
			{
				return Page();
            }

            bool exist = m_UserSecurity.CheckForExistingAppUserId(AppUserData.LoginId);

            if (exist)
            {
                ModelState.AddModelError(string.Empty, "The User " + AppUserData.FullName + "  Can't Be Created. Because the Same Login Id Already Exist");
                return Page();
            }

            if (!AppUserData.RoleId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Please Select the User Role ");
                return Page();
            }

            m_UserSecurity.CreateAppUser(AppUserData, AppUserData.RoleId.Value);

            TempData["Message"] = "New app user has been added successfully.";

			return RedirectToPage(Navigator.ManageUser);
		}
	}
}
