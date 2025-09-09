using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class ShiftManageModel : PageModel
    {
        public IList<ShiftDTO> Usershift { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public ShiftManageModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        public void OnGet()
        {
            Usershift = m_emp.GetAllShift();
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
