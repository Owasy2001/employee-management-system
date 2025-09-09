using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class AddOvertimeModel : PageModel
    {
        [BindProperty]
        public Overtime overtime { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public AddOvertimeModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        public void OnGet()
        {
            overtime=new Overtime()
            {
                OvertimeDate = TimeZoneUtility.DateTimeNow,
            };
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            m_emp.CreateOvertime(overtime);

            TempData["Message"] = "New Overtime user has been added successfully.";

            return RedirectToPage(Navigator.OvertimeManage);
        }
    }
}
