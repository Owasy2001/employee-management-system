using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAASShopDomain.User
{
    public class AppRole
    {
        public AppRole()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }

    public enum Roles
    {
        None = 0,

        [Category("SA")]
        SuperAdmin = 1,  // SAASShop

        [Category("OW")]
        Owner = 2,

        [Category("SS")]
        ShopStaff = 3
    }

    public enum UserGroupEnum
    {
        None = 0,
        PH = 1, // Pharmacy
        TS = 2, // TilesShope
    }
}
