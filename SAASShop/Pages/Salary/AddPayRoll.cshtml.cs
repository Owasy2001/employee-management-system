using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Salary
{
    public class AddPayRollModel : PageModel
    {
        private readonly IPayroll m_pay;
        private readonly IDbLogger m_Log;

        [BindProperty]
        public Payroll PayRoll { get; set; }
        public AddPayRollModel(IPayroll payroll, IDbLogger logger)
        {
            m_pay = payroll;
            m_Log = logger;
        }
        public void OnGet()
        {

            PayRoll = new Payroll()
            {
                PayDate = TimeZoneUtility.DateTimeNow
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool exist = m_pay.CheckForExistingPayRollId(PayRoll.AppUserId, 0);

            if (exist)
            {
                ModelState.AddModelError(string.Empty, "Payroll for this employee is already exist.");
                return Page();
            }

            m_pay.CreatePayroll(PayRoll);

            TempData["Message"] = "New app user has been added successfully.";

            return RedirectToPage(Navigator.SalaryManage);
        }
    }
}
