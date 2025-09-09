using SAASShopDomain.User;
using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain
{
    public class UserRestriction
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "User is Required")]
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public bool RestrictStatus { get; set; }

        #region Audit Info
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [Required, StringLength(50)]
        public string RecordedBy { get; set; }

        [Required]
        public DateTime RecordedOn { get; set; }
        #endregion
    }
}
