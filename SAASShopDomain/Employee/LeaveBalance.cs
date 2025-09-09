using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAASShopDomain
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public int CasualLeaveBalance { get; set; }
        public int SickLeaveBalance { get; set; }
        public int BonusLeaveGiven { get; set; }
        public int AppUserId { get; set; }
    }

    public class LeaveBalanceDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

        public string Email { get; set; }
        public int CasualLeaveBalance { get; set; }
        public int SickLeaveBalance { get; set; }
        public int BonusLeaveGiven { get; set; }

    }
}
