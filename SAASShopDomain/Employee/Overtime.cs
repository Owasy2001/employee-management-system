using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain
{
    public class Overtime
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        [Required(ErrorMessage = "Overtime Hour is Required")]
        public decimal OvertimeHour { get; set; }
        public decimal OvertimeNetSalary { get; set; }
        public DateTime OvertimeDate { get; set; }

    }
    public class OvertimeDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal OvertimeHour { get; set; }
        public decimal OvertimeNetSalary { get; set; }
        public DateTime OvertimeDate { get; set; }

    }
}
