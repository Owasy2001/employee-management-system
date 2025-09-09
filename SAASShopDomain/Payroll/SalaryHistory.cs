namespace SAASShopDomain
{
    public class SalaryHistory
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        //public Employee Employee { get; set; }
        public decimal PreviousSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
