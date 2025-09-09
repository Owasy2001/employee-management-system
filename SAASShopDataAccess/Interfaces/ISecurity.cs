using Microsoft.Data.SqlClient.DataClassification;
using SAASShopDomain;
using SAASShopDomain.User;
using static SAASShopDataAccess.SecurityManager;

namespace SAASShopDataAccess
{
    public interface ISecurity
    {

        AppUser AuthenticateUserCredentials(string userName, string password);

        AppUser GetAppUser(int id);
       
        AppUser GetUserByLoginId(string loginId);

        IList<AppUserDTO> GetAppUsers();

        IList<AppRole> GetAllAppRoles();

        IList<AppUser> GetAllAppUsers();
        int CreateAppUser(AppUser appUserData, int roleId);





        bool DeleteAppUser(int userId);

        bool UpdateAppUser(AppUser appUserData, int roleId);

        bool UpdateAppUser(AppUser appUserData);

        bool ChangePassword(string loginid, string currentPassword, string newPassword);

        bool ResetPassword(string loginid, string newPassword);

        bool CheckForExistingAppUserId(string loginId);
       


        bool ResetPassword(string loginid);

        #region Modules

        bool UpdateAppUserModule(AppUserModule userModule);

        int CreateAppUserModule(AppUserModule userModule);

        AppUserModule GetAppUserModule(int userId, ModuleId moduleId);

        IList<AppUserModuleDTO> GetAppUserModules(int userId);

        #endregion Modules

        IList<AppUserActivity> GetAppUserActivities();

        #region UserRestriction
        int CreateUserRestriction(UserRestriction uerRestriction);
        bool UpdateUserRestriction(UserRestriction uerRestriction);
        UserRestriction GetUserRestriction(int id);
        IList<UserRestrictionDTO> GetAllUserRestrictionList();
        #endregion

        IList<Lov> GetAllMigrationList();
        #region PriVilege
        bool CheckUserPrivilege(int enumValue, int appUserId);

        List<UserPrevilegesDTO> GetAppUserPrivileges(int userId);

        bool UpdateAppUserPrivileges(ModuleId moduleId, int userId, IList<int> toAdd, IList<int> toDelete);

        IList<PrivilegedResource> GetAuthorizedCaseReports(int userId);

        bool ResetUserPrivileges(int appUserId, ModuleId moduleId, bool isPrimarry, List<PrivilegedResource> toDelete);

        IList<PrivilegedResource> GetAuthorizedCannedReports(int userId);

        IList<PrivilegedResource> GetAuthorizedAdhocReports(int userId);

        List<int> GetAppRolePrivileges(int roleId);

        bool UpdateAppRolePrivileges(int roleId, IList<int> toAdd, IList<int> toDelete);

        List<UserPrevilegesDTO> GetAppUserPrivilegesByPrimaryRole(int userId);
        #endregion
    }

}