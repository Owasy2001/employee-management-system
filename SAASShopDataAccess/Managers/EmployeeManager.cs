using Microsoft.EntityFrameworkCore;
using SAASShopDataAccess.DbModel;
using SAASShopDomain;
using SAASShopDomain.User;
using System.Diagnostics;

namespace SAASShopDataAccess
{
    public class EmployeeManager : BaseDataManager, IEmployee
    {
        public EmployeeManager(SAASShopModel context) : base(context)
        {
        }
        #region Shift
        public int CreateShift(Shift shiftdata)
        {
            try
            {
                AddUpdateEntity(shiftdata);
                return shiftdata.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool UpdateShiftUser(Shift ShiftData)
        {
            try
            {
                AddUpdateEntity(ShiftData);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public Shift GetShiftlUser(int id)
        {
            try
            {
                return dbModel.Shifts.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool CheckForExistingShiftId(int AppUserId, int shiftId)
        {
            return GetEntityAny<Shift>(x => x.AppUserId == AppUserId && x.Id != shiftId);
        }
        public IList<ShiftDTO> GetAllShift()
        {
            try
            {
                return (from S in dbModel.Shifts
                        join A in dbModel.AppUsers on S.AppUserId equals A.Id
                        join R in dbModel.AppUserRoles on A.Id equals R.AppUserId
                        join Role in dbModel.AppRoles on R.AppRoleId equals Role.Id
                        select new ShiftDTO
                        {
                            Id = S.Id,
                            AppUserId = A.Id,
                            FullName = A.FullName,
                            Phone = A.Phone,
                            Role = ((Roles)R.AppRoleId).ToString(),
                            Email = A.Email,
                            FromDate = S.FromDate,
                            ToDate = S.ToDate,
                            StartTime = S.StartTime,
                            EndTime = S.EndTime,
                            ShiftName = S.ShiftName
                        }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        #endregion

        #region Overtime
        public int CreateOvertime(Overtime OvertimeData)
        {
            try
            {
                var PayrollData = dbModel.Payrolls.AsNoTracking().FirstOrDefault(x => x.AppUserId == OvertimeData.AppUserId);
                var PayrollSalary = PayrollData.BasicSalary;
                const int standardMonthlyHours = (22 * 8);
                var hourlyRate = PayrollSalary / standardMonthlyHours;
                OvertimeData.OvertimeNetSalary = hourlyRate * OvertimeData.OvertimeHour;
                AddUpdateEntity(OvertimeData);
                return OvertimeData.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool UpdateOvertimeUser(Overtime OverrtimeData)
        {
            try

            {

                var PayrollData = dbModel.Payrolls.AsNoTracking().FirstOrDefault(x => x.AppUserId == OverrtimeData.AppUserId);
                var PayrollSalary = PayrollData.BasicSalary;
                const int standardMonthlyHours = (22 * 8);
                var hourlyRate = PayrollSalary / standardMonthlyHours;
                OverrtimeData.OvertimeNetSalary = hourlyRate * OverrtimeData.OvertimeHour;
                AddUpdateEntity(OverrtimeData);
                dbModel.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public Overtime GetOvertimelUser(int id)
        {
            try
            {
                return dbModel.Overtimes.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public IList<OvertimeDTO> GetAllOvertime()
        {
            try
            {
                return (from OT in dbModel.Overtimes
                        join A in dbModel.AppUsers on OT.AppUserId equals A.Id
                        join R in dbModel.AppUserRoles on A.Id equals R.AppUserId
                        join Role in dbModel.AppRoles on R.AppRoleId equals Role.Id
                        select new OvertimeDTO
                        {
                            Id = OT.Id,
                            AppUserId = A.Id,
                            FullName = A.FullName,
                            Phone = A.Phone,
                            Role = ((Roles)R.AppRoleId).ToString(),
                            Email = A.Email,
                            OvertimeHour = OT.OvertimeHour,
                            OvertimeNetSalary = OT.OvertimeNetSalary,
                            OvertimeDate = OT.OvertimeDate
                        }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        #endregion

        #region Leave
        public int CreateLeave(Leave leaveData)
        {
            try
            {

                AddUpdateEntity(leaveData);
                return leaveData.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool UpdateLeaveUser(Leave leaveData)
        {
            try
            {
                AddUpdateEntity(leaveData);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public Leave GetLeaveUser(int id)
        {
            try
            {
                return dbModel.Leaves.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool CheckForExistingLeaveId(int AppUserId, int leaveId)
        {
            return GetEntityAny<Leave>(x => x.AppUserId == AppUserId && x.Id != leaveId);
        }
        public IList<LeaveDTO> GetAllLeave()
        {
            try
            {
                return (from L in dbModel.Leaves
                        join A in dbModel.AppUsers on L.AppUserId equals A.Id
                        join R in dbModel.AppUserRoles on A.Id equals R.AppUserId
                        join Role in dbModel.AppRoles on R.AppRoleId equals Role.Id
                        select new LeaveDTO
                        {
                            Id = L.Id,
                            AppUserId = A.Id,
                            FullName = A.FullName,
                            Phone = A.Phone,
                            Role = ((Roles)R.AppRoleId).ToString(),
                            Email = A.Email,
                            StartDate = L.StartDate,
                            EndDate = L.EndDate,
                            Reason = L.Reason,
                            LeaveType=L.LeaveType,
                            ApprovesType = L.ApprovesType
                        }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        #endregion

        #region Attendance
        public int CreateAttendance(Attendance attendanceDate)
        {

            try
            {
                AddUpdateEntity(attendanceDate);
                return attendanceDate.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool UpdateAttendance(Attendance attendanceData)
        {
            try
            {
                AddUpdateEntity(attendanceData);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public Attendance GetAttendanceByUserIdAndDate(int appUserId, DateTime date)
        {

            return dbModel.Attendances.FirstOrDefault(a => a.AppUserId == appUserId && a.Date.Date == date.Date && a.CheckOut == null);
        }
        public Attendance GetAttendanceId(int appUserId, DateTime date)
        {

            return dbModel.Attendances.FirstOrDefault(a => a.AppUserId == appUserId && a.Date.Date == date.Date && a.CheckOut != null);
        }
        public Attendance GetAttendance(int id)
        {
            try
            {
                return dbModel.Attendances.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public IList<AttendanceDTO> GetAllAttendance(int id)
        {
            return (from P in dbModel.Attendances
                    join A in dbModel.AppUsers on P.AppUserId equals A.Id
                    where P.AppUserId == id
                    select new AttendanceDTO
                    {
                        Id = P.Id,
                        AppUserId = A.Id,
                        FullName = A.FullName,
                        Phone = A.Phone,
                        Email = A.Email,
                        Date = P.Date,
                        CheckIn = P.CheckIn,
                        CheckOut = P.CheckOut,
                        TotalTimeSpent = P.TotalTimeSpent
                    }).ToList();
        }
        public IList<AttendanceDTO> GetAllAttendanceManage(DateOnly date)
        {
            return (from P in dbModel.Attendances
                    join A in dbModel.AppUsers on P.AppUserId equals A.Id
                    where DateOnly.FromDateTime(P.Date) == date
                    select new AttendanceDTO
                    {
                        Id = P.Id,
                        AppUserId = A.Id,
                        Date = P.Date,
                        CheckIn = P.CheckIn,
                        CheckOut = P.CheckOut,
                        TotalTimeSpent = P.TotalTimeSpent
                    }).ToList();
        }
        public IList<AttendanceDTO> GetAllFilteredAttendance(int id, DateOnly? fromDate, DateOnly? toDate)
        {
            return (from P in dbModel.Attendances
                    join A in dbModel.AppUsers on P.AppUserId equals A.Id
                    where P.AppUserId == id &&
                              (!fromDate.HasValue || DateOnly.FromDateTime(P.Date) >= fromDate.Value) &&
                              (!toDate.HasValue || DateOnly.FromDateTime(P.Date) <= toDate.Value)
                    select new AttendanceDTO
                    {
                        Id = P.Id,
                        AppUserId = A.Id,
                        FullName = A.FullName,
                        Phone = A.Phone,
                        Email = A.Email,
                        Date = P.Date,
                        CheckIn = P.CheckIn,
                        CheckOut = P.CheckOut,
                        TotalTimeSpent = P.TotalTimeSpent
                    }).ToList();
        }
        public IList<AttendanceDTO> GetAllFilteredAttendanceManage(DateOnly? fromDate, DateOnly? toDate)
        {
            return (from P in dbModel.Attendances
                    join A in dbModel.AppUsers on P.AppUserId equals A.Id
                    where (!fromDate.HasValue || DateOnly.FromDateTime(P.Date) >= fromDate.Value) &&
                          (!toDate.HasValue || DateOnly.FromDateTime(P.Date) <= toDate.Value)
                    select new AttendanceDTO
                    {
                        Id = P.Id,
                        AppUserId = A.Id,
                        Date = P.Date,
                        CheckIn = P.CheckIn,
                        CheckOut = P.CheckOut,
                        TotalTimeSpent = P.TotalTimeSpent
                    }).ToList();
        }
        public bool CheckForExistingAttendence(int AppUserId, DateTime dateTime)
        {
            return GetEntityAny<Attendance>(x => x.AppUserId == AppUserId && x.Date.Date == dateTime.Date);
        }
        public bool CheckForAttendenceDone(int AppUserId, DateTime dateTime)
        {
            return GetEntityAny<Attendance>(x => x.AppUserId == AppUserId && x.Date.Date == dateTime.Date && x.CheckOut != null);
        }
        #endregion



        public IList<LeaveBalanceDTO> GetAllLeaveBalance()
        {
            try
            {
                return (from LB in dbModel.LeaveBalances
                        join A in dbModel.AppUsers on LB.AppUserId equals A.Id
                        join R in dbModel.AppUserRoles on A.Id equals R.AppUserId
                        join Role in dbModel.AppRoles on R.AppRoleId equals Role.Id
                        select new LeaveBalanceDTO
                        {
                            Id = LB.Id,
                            AppUserId = A.Id,
                            FullName = A.FullName,
                            Role = ((Roles)R.AppRoleId).ToString(),
                            Email = A.Email,
                            CasualLeaveBalance = LB.CasualLeaveBalance,
                            SickLeaveBalance = LB.SickLeaveBalance,
                            BonusLeaveGiven = LB.BonusLeaveGiven
                        }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool UpdateLeaveBalance(LeaveBalance leaveBalance)
        {
            try
            {
                AddUpdateEntity(leaveBalance);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public LeaveBalance GetLeaveBalance(int id)
        {
            try
            {
                return dbModel.LeaveBalances.FirstOrDefault(x => x.AppUserId == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool CheckForExistingLeaveBalanceId(int AppUserId, int leaveId)
        {
            return GetEntityAny<LeaveBalance>(x => x.AppUserId == AppUserId && x.Id != leaveId);
        }
        public int CreateLeaveBalance(LeaveBalance leaveBalanceData)
        {
            try
            {
                AddUpdateEntity(leaveBalanceData);
                return leaveBalanceData.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
