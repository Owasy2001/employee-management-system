using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Attendance
{
    public class EmployeeAttendenceManageModel : PageModel
    {
        public bool checkFilter = false;

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public EmployeeAttendenceManageModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        [BindProperty]
        public IList<AttendanceDTO> AttendanceList { get; private set; }
        public IList<AttendanceDTO> FilteredAttendanceManage { get; private set; }
        public DateOnly CurrentDate { get; private set; }


        public void OnGet()
        {
            CurrentDate = DateOnly.FromDateTime(TimeZoneUtility.DateTimeNow);
            AttendanceList = m_emp.GetAllAttendanceManage(CurrentDate);
        }

        public IActionResult OnPost()
        {
            if (Request.Form.ContainsKey("filter"))
            {
                checkFilter = true;
                DateOnly fromDate = DateOnly.Parse(Request.Form["fromDate"]);
                DateOnly toDate = DateOnly.Parse(Request.Form["toDate"]);
                if (fromDate > toDate)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "From Date should be less than To Date.");
                    return Page();
                }
                else
                {
                    FilteredAttendanceManage = m_emp.GetAllFilteredAttendanceManage(fromDate, toDate);
                    return Page();
                }
            }
            else
            {
                checkFilter = false;
                CurrentDate = DateOnly.FromDateTime(TimeZoneUtility.DateTimeNow);
                AttendanceList = m_emp.GetAllAttendanceManage(CurrentDate);
                return Page();
            }
        }
    }
}
