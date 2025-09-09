using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAASShopDomain
{
    public class BaseLookup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }
    }

    public class Lov : BaseLookup
    {
    }

    public class LovDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}