using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class AddLeaveBalanceModel : PageModel
    {
        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        [BindProperty]
        public LeaveBalance leaveBalance { get; set; }


        public AddLeaveBalanceModel(IEmployee employee, IDbLogger log)
        {
            m_emp = employee;
            m_Log = log;
        }

        public void OnGet()
        {
            leaveBalance=new LeaveBalance();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if a leave balance already exists for the user
            bool exist = m_emp.CheckForExistingLeaveBalanceId(leaveBalance.AppUserId, leaveBalance.Id);
            if (exist)
            {
                ModelState.AddModelError(string.Empty, "Leave balance for this employee already exists.");
                return Page();
            }

            // Create the new leave balance
            m_emp.CreateLeaveBalance(leaveBalance);
            TempData["Message"] = "Leave balance added successfully.";
            return RedirectToPage("LeaveManage");
        }
    }
}
