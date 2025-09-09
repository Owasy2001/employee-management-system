using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain.User;

namespace SAASShop.Pages.Admin
{
	public class EditAppUserModel : PageModel
	{

		[BindProperty]
		public AppUser AppUserData { get; set; }

		private readonly ISecurity m_UserSecurity;
		private readonly IDbLogger m_Log;
		public EditAppUserModel(ISecurity security, IDbLogger logger)
		{
			m_UserSecurity = security;
			m_Log = logger;

		}

		public void OnGet(int Id)
		{
			try
			{
				AppUserData = m_UserSecurity.GetAppUser(Id);
				AppUserData.RoleId = AppUserData.AppUserRoles.Select(x => x.AppRoleId).FirstOrDefault();
			}
			catch (Exception ex)
			{
				m_Log.CriticalEntry(User.Identity.Name, ex.ToString());
				throw;
			}
        }

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			m_UserSecurity.UpdateAppUser(AppUserData, AppUserData.RoleId.Value);
			TempData["Message"] = "App user has been updated successfully.";

			return RedirectToPage(Navigator.ManageUser);
		}


		public IActionResult OnPostDelete(int listId)
		{
			m_UserSecurity.DeleteAppUser(listId);

			TempData["Message"] = "App user has been deleted successfully.";

			return RedirectToPage(Navigator.ManageUser);
		}

	}
}
