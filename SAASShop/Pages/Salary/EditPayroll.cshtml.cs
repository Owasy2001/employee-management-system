using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Salary
{
    public class EditPayrollModel : PageModel
    {
        private readonly IPayroll m_pay;
        private readonly IDbLogger m_Log;

        [BindProperty]
        public Payroll PayRollData { get; set; }
        public EditPayrollModel(IPayroll payroll, IDbLogger logger)
        {
            m_pay = payroll;
            m_Log = logger;
        }

        public void OnGet(int Id)
        {
           
            try
            {
                PayRollData = m_pay.GetPayrollUser(Id);
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

            bool exist = m_pay.CheckForExistingPayRollId(PayRollData.AppUserId, PayRollData.Id);
            if (exist)
            {
                ModelState.AddModelError(string.Empty, "Payroll for this employee is already exist.");
                return Page();
            }

            m_pay.UpdatePayrollUser(PayRollData);
            TempData["Message"] = "Payroll User has been updated successfully.";

            return RedirectToPage(Navigator.SalaryManage);
        }

        public IActionResult OnPostDelete(int listId)
        {
            m_pay.DeletePayrollData(listId);

            TempData["Message"] = "Payroll User has been deleted successfully.";

            return RedirectToPage(Navigator.SalaryManage);
        }



    }
}
