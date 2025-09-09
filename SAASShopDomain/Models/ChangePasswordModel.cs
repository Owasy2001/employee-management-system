using System.ComponentModel.DataAnnotations;

namespace SAASShop.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Enter your Login ID")]
        public string LoginID { get; set; }

        [Required(ErrorMessage = "Enter your Current Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be a minimum length of 6 and a maximum length of 20")]
        [Required(ErrorMessage = "Enter your New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter your Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}