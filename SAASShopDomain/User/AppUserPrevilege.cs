using SAASShopDomain.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAASShopDomain
{
    public class AppUserPrivilege
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public PrivilegedResource Resource { get; set; }

        [Required]
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public ModuleId ModuleId { get; set; }
    }

    // Application Web Pages
    public enum PrivilegedResource
    {

    }

    public class SystemPrivilege
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public PrivilegedResource Resource { get; set; }

        [Required]
        public int AppRoleId { get; set; }

        public virtual AppRole AppRole { get; set; }
    }
}