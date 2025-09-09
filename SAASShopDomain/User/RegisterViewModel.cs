using SAASShopDomain.User;
using System.ComponentModel.DataAnnotations;
namespace SAASShopDomain
{
    public class RegisterViewModel
    {
        public  int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(pattern: @"^[0-9 ]{11}$", ErrorMessage = "Invalid phone number")]
          public string Phone { get; set; }

        [Required(ErrorMessage = "Login Id is required")]
        public int LoginId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least 8 characters long", MinimumLength = 8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        public string? PhotoPath { get; set; } // PhotoPath is now optional

        public EmployeeRole EmployeeRoles { get; set; }

        public enum EmployeeRole
        {
            // [/*DisplayName*/("Administrator")]
            Administrator = 1,
            ManagingDirector = 2
        }
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

   
}
