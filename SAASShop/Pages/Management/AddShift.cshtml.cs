using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class AddShiftModel : PageModel
    {
        [BindProperty]
        public Shift shift { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public AddShiftModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }
        public void OnGet()
        {

            shift = new Shift()
            {
                FromDate = TimeZoneUtility.DateTimeNow,
                ToDate = TimeZoneUtility.DateTimeNow
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool exist = m_emp.CheckForExistingShiftId(shift.AppUserId, 0);

            if (exist)
            {
                ModelState.AddModelError(string.Empty, "Shift for this employee is already exist.");
                return Page();
            }

            m_emp.CreateShift(shift);

            TempData["Message"] = "New app user for Shift has been added successfully.";

            return RedirectToPage(Navigator.ShiftManage);
        }
    }
}
