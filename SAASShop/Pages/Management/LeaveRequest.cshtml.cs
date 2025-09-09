using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;
namespace SAASShop.Pages.Management
{
    public class LeaveRequestModel : PageModel
    {
        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;
        public IList<LeaveDTO> Userleave { get; set; }
        public LeaveRequestModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }
        public void OnGet()
        {
             Userleave = m_emp.GetAllLeave();
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
