using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain
{
    public class OrgConfiguration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Org Type is required")]
        public OrgType Type { get; set; }

        [Display(Name = "Org Name")]
        [StringLength(100, ErrorMessage = "Name exceeded the max {1} characters")]
        public string Name { get; set; }

        [Display(Name = "Org Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(17)]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        [StringLength(250, ErrorMessage = "Address exceeded the max {1} characters")]
        public string PhysicalAddress { get; set; }

        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "City exceeded the max {1} characters")]
        public string PhysicalCity { get; set; }

        [DataType(DataType.PostalCode)]
        [StringLength(5)]
        public string PhysicalZip { get; set; }

        [StringLength(250, ErrorMessage = "Mailing Address exceeded the max {1} characters")]
        public string MailingAddress { get; set; }

        [StringLength(50, ErrorMessage = "Mailing City exceeded the max {1} characters")]
        public string MailingCity { get; set; }

        [DataType(DataType.PostalCode)]
        [StringLength(5)]
        public string MailingZip { get; set; }
        public int StateId { get; set; }
    }

    public enum OrgType
    {
        [Display(Name = "Shop")]
        ChildWelfare = 1,
    }
}
