using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain
{
    public class SystemConfiguration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public ConfigurationCode Code { get; set; }

        public int Value { get; set; }
        public bool IsActive { get; set; }
    }

    public enum ConfigurationCode
    {
        [Display(GroupName = "Password", Name = "Password Length")]
        PasswordLength = 0x0101,

        [Display(GroupName = "RecordLocking", Name = "Lock Time For Interview Note", Description = "Lock Time For Interview Note")]
        LockTimeForInterviewNote = 0x0301,

        [Display(GroupName = "RecordLocking", Name = "Lock Time For Case Note", Description = "Lock Time For Case Note")]
        LockTimeForCaseNote = 0x0302,

        [Display(GroupName = "Accessibility", Name = "Open Access to Case", Description = "Open Access to Case")]
        OpenAccess = 0x0501, // Workers must be assigned to have access to Cases

        // Name = how it's displayed on table. hex 1282
        [Display(GroupName = "Expiration", Name = "Days Until License Expires", Description = "Days Until License Expires")]
        ValidDaysForLicense = 0x0502,

        [Display(GroupName = "MFA", Name = "Multi-Factor Authentication", Description = "Multi-Factor Authentication")]
        EnforceMFA = 0x0701, // Workers must be assigned to have access to Cases
    }
}