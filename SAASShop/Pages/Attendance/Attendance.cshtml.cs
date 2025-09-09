using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;

namespace SAASShop.Pages.Attendance
{
    public class AttendanceModel : PageModel
    {
        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        public AttendanceModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        public DateTime attendanceDate { get; set; }
        public DateTime attendanceTime { get; set; }

        [BindProperty]
        public SAASShopDomain.Attendance attendance { get; set; }
        public SAASShopDomain.Attendance attendanceCheckOut { get; set; }

        public bool Exist { get; set; }
        public bool ExistCheckOut { get; set; }
        public void OnGet()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

            attendanceDate = TimeZoneUtility.DateTimeNow;

            Exist = m_emp.CheckForExistingAttendence(userId, attendanceDate);
            ExistCheckOut = m_emp.CheckForAttendenceDone(userId, attendanceDate);

            if (ExistCheckOut)
            {
                var existingAttendance = m_emp.GetAttendanceId(userId, attendanceDate);

                if (existingAttendance != null)
                {
                    int attendanceId = existingAttendance.Id;
                    attendanceCheckOut = m_emp.GetAttendance(attendanceId);
                    TempData["Message"] = "You Check in at : " + attendanceCheckOut.CheckIn.ToString("hh:mm tt") + " and Check out at : " + attendanceCheckOut.CheckOut?.ToString("hh:mm tt");
                }
                else
                {
                    TempData["Message"] = "Check out successfully";
                }
            }
        }

        public IActionResult OnPost()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

            attendance.AppUserId = userId;

            if (Request.Form.ContainsKey("check_in"))
            {
                attendanceDate = TimeZoneUtility.DateTimeNow;
                bool exist = m_emp.CheckForExistingAttendence(userId, attendanceDate);

                if (!exist)
                {
                    Exist = true;
                    attendanceDate = TimeZoneUtility.DateTimeNow;
                    attendanceTime = TimeZoneUtility.DateTimeNow;
                    attendance.Date = TimeZoneUtility.DateTimeNow;
                    attendance.CheckIn = TimeZoneUtility.DateTimeNow;

                    m_emp.CreateAttendance(attendance);
                    TempData["Message"] = "Attendance has been added successfully.";
                    return RedirectToPage(Navigator.EmployeeAttendence);
                }
                else
                {
                    TempData["Message"] = "FAILED.";
                    return Page();
                }

            }
            else if (Request.Form.ContainsKey("check_out"))
            {
                var existingAttendance = m_emp.GetAttendanceByUserIdAndDate(userId, TimeZoneUtility.DateTimeNow);
                if (existingAttendance != null)
                {
                    int attendanceId = existingAttendance.Id;
                    existingAttendance.CheckOut = TimeZoneUtility.DateTimeNow; ;
                    existingAttendance.TotalTimeSpent = (float)(
                     existingAttendance.CheckOut.Value.Hour - existingAttendance.CheckIn.Hour
                     + (existingAttendance.CheckOut.Value.Minute - existingAttendance.CheckIn.Minute) / 60.0
                     + (existingAttendance.CheckOut.Value.Second - existingAttendance.CheckIn.Second) / 3600.0
 );



                    m_emp.UpdateAttendance(existingAttendance);
                    TempData["Message"] = "Checked out successfully.";
                    return RedirectToPage(Navigator.EmployeeAttendence);
                }
                return Page();
            }
            return Page();
        }
    }
}

