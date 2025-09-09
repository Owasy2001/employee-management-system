using SAASShopDomain;

namespace SAASShopDataAccess
{
    public interface IEmployee
    {
        #region Shift
        public int CreateShift(Shift shiftdata);

        bool UpdateShiftUser(Shift ShiftData);

        Shift GetShiftlUser(int id);

        bool CheckForExistingShiftId(int AppUserId, int shiftId);

        public IList<ShiftDTO> GetAllShift();
        #endregion

        #region Overtime
        int CreateOvertime(Overtime overtimeData);

        bool UpdateOvertimeUser(Overtime OverrtimeData);

        Overtime GetOvertimelUser(int id);

        IList<OvertimeDTO> GetAllOvertime();
        #endregion

        #region Leave
        int CreateLeave(Leave LeaveData);

        bool UpdateLeaveUser(Leave leaveData);

        Leave GetLeaveUser(int id);

        public bool CheckForExistingLeaveId(int AppUserId, int leaveId);

        IList<LeaveDTO> GetAllLeave();
        #endregion

        #region Attendance
        int CreateAttendance(Attendance attendanceDate);

        bool UpdateAttendance(Attendance attendanceDate);

        Attendance GetAttendanceByUserIdAndDate(int appUserId, DateTime date);

        Attendance GetAttendanceId(int appUserId, DateTime date);

        Attendance GetAttendance(int id);

        IList<AttendanceDTO> GetAllAttendance(int id);

        IList<AttendanceDTO> GetAllAttendanceManage(DateOnly date);

        IList<AttendanceDTO> GetAllFilteredAttendance(int id, DateOnly? fromDate, DateOnly? toDate);

        IList<AttendanceDTO> GetAllFilteredAttendanceManage(DateOnly? fromDate, DateOnly? toDate);

        bool CheckForExistingAttendence(int AppUserId, DateTime dateTime);

        bool CheckForAttendenceDone(int AppUserId, DateTime dateTime);
        #endregion


        IList<LeaveBalanceDTO> GetAllLeaveBalance();
        bool UpdateLeaveBalance(LeaveBalance leaveBalance);
        LeaveBalance GetLeaveBalance(int id);
        bool CheckForExistingLeaveBalanceId(int AppUserId, int leaveId);
        int CreateLeaveBalance(LeaveBalance leaveBalanceData);
    }
}
