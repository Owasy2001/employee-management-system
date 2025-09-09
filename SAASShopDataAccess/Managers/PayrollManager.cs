using Microsoft.EntityFrameworkCore;
using SAASShopDataAccess.DbModel;
using SAASShopDomain;
using SAASShopDomain.User;
using System.Diagnostics;

namespace SAASShopDataAccess
{
    public class PayrollManager : BaseDataManager, IPayroll
    {
        public PayrollManager(SAASShopModel context) : base(context)
        {
        }
        public int CreatePayroll(Payroll payrollData)
        {
            try
            {
                payrollData.NetSalary = payrollData.BasicSalary + payrollData.Bonus - payrollData.Deductions;

                AddUpdateEntity(payrollData);
                return payrollData.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool UpdatePayrollUser(Payroll payrollData)
        {
            try
            {
                var CurrentSalary = payrollData.BasicSalary + payrollData.Bonus - payrollData.Deductions;

                var previousPayroll = dbModel.Payrolls.AsNoTracking().FirstOrDefault(x => x.Id == payrollData.Id);
                var previousSalary = previousPayroll.NetSalary;


                payrollData.NetSalary = CurrentSalary;

                AddUpdateEntity(payrollData);


                if (previousSalary != CurrentSalary)
                {
                    var salaryHistory = new SalaryHistory
                    {
                        AppUserId = payrollData.AppUserId,
                        PreviousSalary = previousSalary,
                        NewSalary = payrollData.NetSalary,
                        EffectiveDate = DateTime.Now
                    };

                    dbModel.SalaryHistorys.Add(salaryHistory);
                    dbModel.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public Payroll GetPayrollUser(int id)
        {
            try
            {
                return dbModel.Payrolls.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public IList<PayrollDTO> GetAllPayroll()
        {
            try
            {
                return (from P in dbModel.Payrolls
                        join A in dbModel.AppUsers on P.AppUserId equals A.Id
                        join R in dbModel.AppUserRoles on A.Id equals R.AppUserId
                        join Role in dbModel.AppRoles on R.AppRoleId equals Role.Id
                        select new PayrollDTO
                        {
                            Id = P.Id,
                            AppUserId = A.Id,
                            FullName = A.FullName,
                            Phone = A.Phone,
                            Role = ((Roles)R.AppRoleId).ToString(),
                            Email = A.Email,
                            BasicSalary = P.BasicSalary,
                            Bonus = P.Bonus,
                            Deductions = P.Deductions,
                            NetSalary = P.NetSalary,
                            PayDate = P.PayDate
                        }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool DeletePayrollData(int listId)
        {
            var payrollEntity = dbModel.Payrolls.FirstOrDefault(p => p.Id == listId);
            if (payrollEntity != null)
            {
                dbModel.Payrolls.Remove(payrollEntity);
            }
            dbModel.SaveChanges();
            return true;

        }
        public bool CheckForExistingPayRollId(int AppUserId, int payrollId)
        {
            return GetEntityAny<Payroll>(x => x.AppUserId == AppUserId && x.Id != payrollId);
        }
    }
}
