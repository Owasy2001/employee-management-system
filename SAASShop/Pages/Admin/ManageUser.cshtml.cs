using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;
using SAASShopDomain.User;

namespace SAASShop.Pages.Admin
{
    public class ManageUserModel : PageModel
    {

        private readonly ISecurity m_UserSecurity;
        private readonly IDbLogger m_Log;

        public IList<AppUserDTO> Users { get; set; }

        public ManageUserModel(ISecurity security, IDbLogger logger)
        {
            m_UserSecurity = security;
            m_Log = logger;
        }
        public void OnGet()
        {
            Users = m_UserSecurity.GetAppUsers();

        }
    }
}
