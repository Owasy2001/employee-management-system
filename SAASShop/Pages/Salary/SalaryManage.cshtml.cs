using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Salary
{
    public class SalaryManageModel : PageModel
    {
        private readonly IPayroll m_pay;
        private readonly IDbLogger m_Log;
        public IList<PayrollDTO> Userpayroll { get; set; }

        public SalaryManageModel(IPayroll payroll, IDbLogger logger)
        {
            m_pay = payroll;
            m_Log = logger;
        }
        public void OnGet()
        {
            Userpayroll = m_pay.GetAllPayroll();

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Success");
        }
    }
}
