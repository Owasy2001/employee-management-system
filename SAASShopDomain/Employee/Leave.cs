using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAASShopDomain
{
    public class Leave
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        //public AppUser Employee { get; set;}
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }

        public Leaves LeaveType { get; set; }
        public Approves ApprovesType { get; set; }
    }

    public enum Leaves
    {
        SelectCategory = 0,

        [Category("CL")]
        CasualLeave = 1,

        [Category("SL")]
        SickLeave = 2,

        [Category("BL")]
        BonusLeave = 3
    }

    public enum Approves
    {
        SelectCategory = 0,

        [Category("AP")]
        Approved = 1,

        [Category("NAP")]
        NotApproved = 2,


    }
    public class LeaveDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public Leaves LeaveType { get; set; }
        public Approves ApprovesType { get; set; }

    }
}
