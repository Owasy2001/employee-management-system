using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class AddLeaveModel : PageModel
    {
        [BindProperty]
        public Leave leave { get; set; }
         [BindProperty]
        public LeaveBalance LeaveBalance { get; set; }
        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public AddLeaveModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }
        public void OnGet()
        {
            int UId = int.Parse(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
            LeaveBalance = m_emp.GetLeaveBalance(1);
            leave = new Leave()
            {
                StartDate = TimeZoneUtility.DateTimeNow,
                EndDate = TimeZoneUtility.DateTimeNow,
            };
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool exist = m_emp.CheckForExistingLeaveId(leave.AppUserId, 0);

            if (exist)
            {
                ModelState.AddModelError(string.Empty, "Leave for this employee is already exist.");
                return Page();
            }

            m_emp.CreateLeave(leave);

            TempData["Message"] = "New leave user has been added successfully.";

            return RedirectToPage(Navigator.LeaveManage);
        }
    }
}
