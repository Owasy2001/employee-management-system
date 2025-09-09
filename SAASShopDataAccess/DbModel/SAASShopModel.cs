using Microsoft.EntityFrameworkCore;
using SAASShopDomain;
using SAASShopDomain;
using SAASShopDomain;
using SAASShopDomain.User;

namespace SAASShopDataAccess.DbModel
{
    public partial class SAASShopModel : DbContext
    {
        public SAASShopModel()
        {
        }
        public SAASShopModel(DbContextOptions<SAASShopModel> options) : base(options)
        {
        }

        #region All Entities

        #region User

        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppUserRole> AppUserRoles { get; set; }
        public virtual DbSet<AppUserModule> AppUserModules { get; set; }
        public virtual DbSet<AppUserActivity> AppUserActivitys { get; set; }
        public virtual DbSet<UserRestriction> UserRestrictions { get; set; }

        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<SalaryHistory> SalaryHistorys { get; set; }
        public virtual DbSet<LeaveBalance> LeaveBalances { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }

       
        public virtual DbSet<Overtime> Overtimes { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        #endregion User

        #region Config
        public virtual DbSet<OrgConfiguration> OrgConfigurations { get; set; }
        public virtual DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public virtual DbSet<SystemPrivilege> SystemPrivileges { get; set; }
        public virtual DbSet<AppUserPrivilege> AppUserPrivileges { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<SAASShopProduct> SAASShopProducts { get; set; }

        #endregion

        #region Looksups
        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserRole>()
            .HasKey(bc => new { bc.AppUserId, bc.AppRoleId });

            modelBuilder.Entity<AppUserRole>()
               .HasOne(bc => bc.AppUser)
               .WithMany(b => b.AppUserRoles)
               .HasForeignKey(bc => bc.AppUserId);

            modelBuilder.Entity<AppUserRole>()
               .HasOne(bc => bc.AppRole)
               .WithMany(b => b.AppUserRoles)
               .HasForeignKey(bc => bc.AppRoleId);

            base.OnModelCreating(modelBuilder);

            #region DTOs
            modelBuilder.Entity<AppUserDTO>().HasNoKey().ToView("AppUserDTO");
            modelBuilder.Entity<PayrollDTO>().HasNoKey().ToView("PayrollDTO");
            modelBuilder.Entity<UserPrevilegesDTO>().HasNoKey().ToView("UserPrevilegesDTO");
            modelBuilder.Entity<UserRestrictionDTO>().HasNoKey().ToView("UserRestrictionDTO");

            #endregion
        }
    }
}