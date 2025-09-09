using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;
namespace SAASShop.Pages.Management
{
    public class EditLeaveBalanceModel : PageModel
    {
        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;
        [BindProperty]
        public LeaveBalance leaveBalance { get; set; }

        public EditLeaveBalanceModel(IEmployee employee, IDbLogger log)
        {
            m_emp = employee;
            m_Log = log;
        }

        public void OnGet(int Id)
        {
            try
            {
                leaveBalance = m_emp.GetLeaveBalance(Id);
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
            //bool exist = m_emp.CheckForExistingLeavelId(leaveBalance.AppUserId, leaveBalance.Id);
            //if (exist)
            //{
            //    ModelState.AddModelError(string.Empty, "Leaves for this employee is already exist.");
            //    return Page();
            //}

            m_emp.UpdateLeaveBalance(leaveBalance);
            TempData["Message"] = "Leave balance has been updated successfully.";
            return RedirectToPage("LeaveManage");
        }

    }
}
