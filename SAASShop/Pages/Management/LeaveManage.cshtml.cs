

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SAASShopDataAccess;
using SAASShopDomain;
namespace SAASShop.Pages.Management
{
    public class LeaveManageModel : PageModel
    {
        public IList<LeaveDTO> Userleave { get; set; }
        public IList<LeaveBalanceDTO> UserLeaveBalances { get; set; }

        private readonly IEmployee m_emp;
        private readonly IDbLogger m_Log;

        [BindProperty]
        public Leave LeaveData { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsShow { get; set; }
        public LeaveManageModel(IEmployee employee, IDbLogger logger)
        {
            m_emp = employee;
            m_Log = logger;
        }

        public void OnGet()
        {
            Userleave = m_emp.GetAllLeave();
            UserLeaveBalances = m_emp.GetAllLeaveBalance();
        }

        public IActionResult OnPostShow(int listId)
        {
            LeaveData = m_emp.GetLeaveUser(listId);
            if (LeaveData == null)
            {
                TempData["Message"] = "Leave request not found.";
                return RedirectToPage();
            }

            IsShow = true;
            Userleave = m_emp.GetAllLeave();
            UserLeaveBalances = m_emp.GetAllLeaveBalance();
            return Page();
        }




        public IActionResult OnPostApprove(int leaveId)
        {
            // Fetch the leave request based on leaveId
            var leave = m_emp.GetLeaveUser(leaveId);
            if (leave == null)
            {
                TempData["Message"] = "Leave request not found.";
                return RedirectToPage();
            }

            // Fetch the user's leave balance from the UserLeaveBalances list
            var leaveBalance = m_emp.GetLeaveBalance(leave.AppUserId);
            if (leaveBalance == null)
            {
                TempData["Message"] = "Leave balance not found for the user.";
                return RedirectToPage();
            }

            // Calculate the number of leave days
            int leaveDays = (leave.EndDate - leave.StartDate).Days;

            // Validate if the leave period is valid
            if (leave.StartDate > leave.EndDate)
            {
                TempData["Message"] = "Start date cannot be after end date.";
                return RedirectToPage();
            }

            // Approve leave based on leave type
            switch (leave.LeaveType)
            {
                case Leaves.CasualLeave:
                    if (leaveBalance.CasualLeaveBalance >= leaveDays)
                    {
                        leaveBalance.CasualLeaveBalance -= leaveDays;
                    }
                    else
                    {
                        TempData["Message"] = "Insufficient casual leave balance.";
                        return RedirectToPage();
                    }
                    break;

                case Leaves.SickLeave:
                    if (leaveBalance.SickLeaveBalance >= leaveDays)
                    {
                        leaveBalance.SickLeaveBalance -= leaveDays;
                    }
                    else
                    {
                        TempData["Message"] = "Insufficient sick leave balance.";
                        return RedirectToPage();
                    }
                    break;

                case Leaves.BonusLeave:
                    leaveBalance.BonusLeaveGiven += leaveDays;
                    break;

                default:
                    TempData["Message"] = "Invalid leave type.";
                    return RedirectToPage();
            }

            // Update the leave balance and the leave approval status
            m_emp.UpdateLeaveBalance(leaveBalance);
            leave.ApprovesType = Approves.Approved;
            m_emp.UpdateLeaveUser(leave);

            TempData["Message"] = "Leave request approved successfully.";
            return RedirectToPage();
        }



    }
}
