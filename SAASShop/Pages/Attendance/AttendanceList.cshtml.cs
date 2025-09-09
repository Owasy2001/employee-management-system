using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;

namespace SAASShop.Pages.Attendance
{
    public class AttendanceListModel : PageModel
    {
        public bool checkFilter = false;

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public AttendanceListModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }
        public IList<AttendanceDTO> AttendanceList { get; private set; }
        public IList<AttendanceDTO> FilteredAttendanceList { get; private set; }
        public IList<DateTime> MissingDates { get; private set; }

        public void OnGet()
        {
            var userIdClaim = User.Claims.FirstOrDefault(s => s.Type == "UserId")?.Value;

            int userId = int.Parse(userIdClaim);
            AttendanceList = m_emp.GetAllAttendance(userId);
        }

        public IActionResult OnPost()
        {
            if (Request.Form.ContainsKey("filter"))
            {
                checkFilter = true;
                int userId = int.Parse(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

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
                    FilteredAttendanceList = m_emp.GetAllFilteredAttendance(userId, fromDate, toDate);
                    MissingDates = GetMissingDates(fromDate, toDate, FilteredAttendanceList);
                    return Page();
                }
            }
            else
            {
                checkFilter = false;
                var userIdClaim = User.Claims.FirstOrDefault(s => s.Type == "UserId")?.Value;
                int userId = int.Parse(userIdClaim);
                AttendanceList = m_emp.GetAllAttendance(userId);
                return Page();
            }
        }

        private IList<DateTime> GetMissingDates(DateOnly fromDate, DateOnly toDate, IList<AttendanceDTO> attendanceList)
        {
            var fromDateTime = fromDate.ToDateTime(TimeOnly.MinValue);
            var toDateTime = toDate.ToDateTime(TimeOnly.MinValue);

            var allDates = Enumerable.Range(0, (toDateTime - fromDateTime).Days + 1)
                                     .Select(offset => fromDateTime.AddDays(offset))
                                     .ToList();

            var attendanceDates = attendanceList.Select(a => a.Date.Date).ToList();

            return allDates.Except(attendanceDates).ToList();
        }
    }
}
