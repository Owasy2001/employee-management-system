using SAASShopDomain.User;
using System;

namespace SAASShopDomain
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public double? TotalTimeSpent { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }

    public class AttendanceDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public double? TotalTimeSpent { get; set; }
    }
}
