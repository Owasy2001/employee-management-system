using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Management
{
    public class EditShiftModel : PageModel
    {
        [BindProperty]
        public Shift ShiftData { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public EditShiftModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        public void OnGet(int Id)
        {

            try
            {
                ShiftData = m_emp.GetShiftlUser(Id);
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

            bool exist = m_emp.CheckForExistingShiftId(ShiftData.AppUserId, ShiftData.Id);
            if (exist)
            {
                ModelState.AddModelError(string.Empty, "Shift for this employee is already exist.");
                return Page();
            }

            m_emp.UpdateShiftUser(ShiftData);
            TempData["Message"] = "Shift User has been updated successfully.";

            return RedirectToPage(Navigator.ShiftManage);
        }
    }
}
