using SAASShopDomain.User;
using System.ComponentModel;

namespace SAASShopDomain
{
    public class Department
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public DepartmentEnum CurrentDepartment { get; set; }
        public DepartmentEnum PreviousDepartment { get; set; }
        public virtual AppUser AppUser { get; set; }

    }

    public enum DepartmentEnum
    {
        None = 0,
        [Category("HR")]
        HumanResources = 1,
        [Category("IT")]
        InformationTechnology = 2,
        [Category("FIN")]
        Finance = 3,
        [Category("MKT")]
        Marketing = 4
    }
}
