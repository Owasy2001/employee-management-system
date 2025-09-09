using System.ComponentModel.DataAnnotations;

namespace SAASShopDomain
{
    public class SAASShopProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name exceeded the max {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Version is required")]
        [StringLength(100, ErrorMessage = "Version exceeded the max {1} characters")]
        public string Version { get; set; }

        [Required(ErrorMessage = "Display Name is required")]
        [StringLength(100, ErrorMessage = "Display Name exceeded the max {1} characters")]
        public string DisplayName { get; set; }

        [StringLength(100, ErrorMessage = "Server Name exceeded the max {1} characters")]
        public string ServerName { get; set; }

        [Required(ErrorMessage = "Last Updated is required")]
        public DateTime LastUpdated { get; set; }
    }
}