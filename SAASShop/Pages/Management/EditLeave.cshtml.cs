using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class EditLeaveModel : PageModel
    {
        [BindProperty]
        public Leave LeaveData { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public EditLeaveModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        public void OnGet(int Id)
        {

            try
            {
                LeaveData = m_emp.GetLeaveUser(Id);
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

            m_emp.UpdateLeaveUser(LeaveData);
            TempData["Message"] = "Leaves User has been updated successfully.";
            return RedirectToPage(Navigator.LeaveRequest);
        }
    }
}
