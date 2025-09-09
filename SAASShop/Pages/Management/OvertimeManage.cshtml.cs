using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class OvertimeManageModel : PageModel
    {
        public IList<OvertimeDTO> UserOvertime { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public OvertimeManageModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }
        public void OnGet()
        {
            UserOvertime = m_emp.GetAllOvertime();
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
