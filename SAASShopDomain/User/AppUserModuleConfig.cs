using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace SAASShopDomain.User
{
    public abstract class AppUserModuleBase
    {
        public int Id { get; set; }

        public ModuleId ModuleId { get; set; }
        public bool HasAccess { get; set; }

    }

    public class AppUserModule : AppUserModuleBase
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
    }

    public class AppUserModuleDTO : AppUserModuleBase
    {
        public int AppUserId { get; set; }
        public int AppRoleId { get; set; }
        public bool IsPrimary { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
    }

    public class RemoveAppUserModuleDTO
    {
        public int AppUserId { get; set; }
        public ModuleId ModuleId { get; set; }
        public AccessActionType AccessType { get; set; }
    }

    public enum AccessActionType
    {
        CanAccess = 1,
        RemoveAccessTemporarily = 2,
        RemoveAccessPermanently = 3
    }

    public enum ModuleId
    {
        [Display(Name = "General"), Description("General"), Category("None")]
        None = 0,

        [Display(Name = "Pharmecy"), Description("Pharmecy"), Category("PH")]
        PH = 1,

        [Display(Name = "Tiles"), Description("Tiles"), Category("TS")]
        TS = 2,
			}
}
