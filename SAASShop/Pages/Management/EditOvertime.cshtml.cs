using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class EditOvertimeModel : PageModel
    {
        [BindProperty]
        public Overtime OvertimeData { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public EditOvertimeModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        public void OnGet(int Id)
        {

            try
            {
                OvertimeData = m_emp.GetOvertimelUser(Id);
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
           
            m_emp.UpdateOvertimeUser(OvertimeData);
            TempData["Message"] = "Overtime User has been updated successfully.";

            return RedirectToPage(Navigator.OvertimeManage);
        }
    }
}
