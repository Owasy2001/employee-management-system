using SAASShopDomain;

namespace SAASShopDataAccess
{
    public interface IPayroll
    {
        int CreatePayroll(Payroll payrollData);

        bool UpdatePayrollUser(Payroll payroll);

        Payroll GetPayrollUser(int id);

        IList<PayrollDTO> GetAllPayroll();

        bool DeletePayrollData(int userId);

        bool CheckForExistingPayRollId(int AppUserId, int payrollId);
    }
}
