using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain
{
    public class Shift
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "Shift Name is Required")]
        public string ShiftName { get; set; }
    }

    public class ShiftDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string ShiftName { get; set; }
    }
}
