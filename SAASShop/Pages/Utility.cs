using SAASShopDomain.User;

namespace SAASShop.Pages
{
    public class MenuModel
    {
        public int CaseId { get; set; }
        public string Identifier { get; set; }
        public string CssClass { get; set; }
        public bool ReadOnly { get; set; }
    }

    public class ReportMenuModel
    {
        public int ReportId { get; set; }
        public string Identifier { get; set; }
        public bool IsReadOnly { get; set; }
    }

    public class CommitCancelModel
    {
        public string CancelUrl { get; set; }
        public string CommitValue { get; set; }
        public bool SaveDisabled { get; set; }
        public Roles UserRole { get; set; }
    }

    public class DeleteCancelModel
    {
        public string CancelUrl { get; set; }
        public int ListId { get; set; }
    }

    public class RecordingModel
    {
        public string RecordedBy { get; set; }
        public DateTime RecordedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class CheckBoxControlModel
    {
        public string ControlId { get; set; }
        public bool IsChecked { get; set; }
        public string Label { get; set; }
        public string ClickEvent { get; set; }
    }

    public class MessageModel
    {
        public object Message { get; set; }
        public object Type { get; set; }
    }
    public class Navigator
    {
        #region Security

        public const string Login = "/Security/Login";
        public const string ChangePassword = "/Security/ChangePassword";
        //public const string UserPrivileges = "/Admin/UserPrivileges";
        public const string UserProfile = "/Security/UserProfile";
        //public const string ManageSystemPrivileges = "/Admin/ManageSystemPrivileges";
        //public const string MigrationHistory = "/Admin/MigrationHistory";
        public const string VerifyMFA = "/Security/MFAVerification";
        public const string MFAOption = "/Security/MFAOption";
        public const string ResetNewPassword = "/Security/ResetNewPassword";
        public const string LoginDetail = "/Security/LoginDetail";
        #endregion Security


        #region User
        public const string PhDashboard = "/Pharmacy/User/Dashboard";
        public const string TSDashboard = "/Tiles/User/Dashboard";
        public const string Dashboard = "/Admin/Dashboard";
        public const string ManageUser = "/Admin/ManageUser";
        public const string AddAppUser = "/Admin/AddAppUser";
        public const string EditAppUser = "/Admin/EditAppUser";

        public const string PayRoll = "/Salary/AddPayRoll";
        public const string EditPayRoll = "/Salary/EditPayroll";
        public const string SalaryManage = "/Salary/SalaryManage";
        public const string LeaveManage = "/Management/LeaveManage";
        public const string AddLeave = "/Management/AddLeave";
        public const string EditLeave = "/Management/EditLeave";
        public const string AddOvertime = "/Management/AddOvertime";
        public const string EditOvertime = "/Management/EditOvertime";
        public const string OvertimeManage = "/Management/OvertimeManage";
        public const string ShiftManage = "/Management/ShiftManage";
        public const string AddShift = "/Management/AddShift";
        public const string EditShift = "/Management/EditShift";

        public const string EmployeeManage = "/Security/EmployeeManage";
        public const string EmployeeAttendence = "/Attendance/Attendance";
        public const string EmployeeAttendenceList = "/Attendance/AttendanceList";
        public const string EmployeeAttendenceManage = "/Attendance/EmployeeAttendenceManage";

        public const string LeaveRequest = "/Management/LeaveRequest";
        public const string AddLeaveBalance = "/Management/AddLeaveBalance";
        public const string EditLeaveBalance = "/Management/EditLeaveBalance";
        #endregion
    }

    public class Utility
    {
        #region Executive

        public const string CanDelete = "CanDelete";
        public const string CanApprove = "CanApprove";
        public const string CanUnlock = "CanUnlock";
        public const string CanAcknowledge = "CanAcknowledge";
        #endregion Executive
    }
}