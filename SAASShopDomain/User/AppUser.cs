using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain.User
{
    public class AppUser
    {
        public AppUser()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string LoginId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be a minimum length of 6 and a maximum length of 20")]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(17)]
        [RegularExpression(pattern: @"^[0-9 ]{11}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not a valid")]
        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [NotMapped]
        public int? RoleId { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
