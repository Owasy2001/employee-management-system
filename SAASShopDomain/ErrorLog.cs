using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain
{
    public class ErrorLog
    {
        public int Id { get; set; }

        [StringLength(8000)]
        public string Error { get; set; }

        [StringLength(1000)]
        public string ErrorSource { get; set; }

        public DateTime LoggedOn { get; set; }

        [StringLength(100)]
        public string LoggedBy { get; set; }
    }
}