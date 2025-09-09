namespace SAASShopDomain
{
    public class Payroll
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PayDate { get; set; }
    }

    public class PayrollDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PayDate { get; set; }
    }


}
