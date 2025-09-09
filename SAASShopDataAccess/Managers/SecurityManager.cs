using CommonLib;
using Microsoft.EntityFrameworkCore;
using SAASShopDataAccess.DbModel;
using SAASShopDomain;
using SAASShopDomain.User;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization.Metadata;

namespace SAASShopDataAccess
{
    public class SecurityManager : BaseDataManager, ISecurity
    {
        public SecurityManager(SAASShopModel context) : base(context)
        {
        }
        public AppUser AuthenticateUserCredentials(string userName, string password)
        {
            string pwd = EncryptPassword(password);

            var userData = dbModel.AppUsers.Include(x => x.AppUserRoles).ThenInclude(appUser => appUser.AppRole)
                    .FirstOrDefault(x => x.LoginId == userName);

            if (userData != null)
            {
                if (userData.Password == pwd)
                {
                    var AppUserActivity = new AppUserActivity
                    {
                        LastActiveOn = TimeZoneUtility.DateTimeNow,
                        AppUserId = userData.Id,
                        LoginId = userData.LoginId
                    };

                    AddUpdateEntity(AppUserActivity);
                }
                else
                    return null;

            }

            return userData;
        }

        public AppUser GetAppUser(int id)
        {
            try
            {
                return dbModel.AppUsers.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }


        


        public AppUser GetUserByLoginId(string loginId)
        {
            try
            {
                return dbModel.AppUsers.Where(x => x.LoginId.ToUpper() == loginId.ToUpper()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }

        }

        public bool UpdateAppUser(AppUser appUserData, int roleId)
        {
            try
            {
                AddUpdateEntity(appUserData);

                var role = dbModel.AppUserRoles.FirstOrDefault(x => x.AppUserId == appUserData.Id);

                if (role.AppRoleId != roleId)
                {
                    dbModel.AppUserRoles.Remove(role);
                    dbModel.AppUserRoles.Add(new AppUserRole { AppRoleId = roleId, AppUserId = appUserData.Id });
                }

                dbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }




        public bool DeleteAppUser(int userId)
        {
            RemoveEntity<AppUser>(userId);

            var role = dbModel.AppUserRoles.FirstOrDefault(x => x.AppUserId == userId);

            if (role != null)
            {
                dbModel.AppUserRoles.Remove(role);
            }

            dbModel.SaveChanges();
            return true;
        }








        public bool UpdateAppUser(AppUser appUserData)
        {
            return AddUpdateEntity(appUserData);
        }

      
      
        public IList<AppUserDTO> GetAppUsers()
        {
            try
            {
                return (from A in dbModel.AppUsers
                        join R in dbModel.AppUserRoles on A.Id equals R.AppUserId
                        select new AppUserDTO
                        {
                            Id = A.Id,
                            LoginId = A.LoginId,
                            FirstName = A.FirstName,
                            LastName = A.LastName,
                            Phone = A.Phone,
                            Email = A.Email,
                            Role = ((Roles)R.AppRoleId).ToString(),
                            AppUserRoleDesc = R.AppRole.Description,
                        }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public IList<AppRole> GetAllAppRoles()
        {
            return GetEntityListData<AppRole>();
        }

        public IList<AppUser> GetAllAppUsers()
        {
            return GetEntityListData<AppUser>();


        }
        public int CreateAppUser(AppUser appUserData, int roleId)
        {
            try
            {
                AppRole role = dbModel.AppRoles.Find(roleId);

                AppUserRole appUserRole = new AppUserRole
                {
                    AppRole = role,
                    AppUser = appUserData
                };

                string pwd = EncryptPassword(appUserData.Password);
                appUserData.Password = pwd;

                appUserData.AppUserRoles.Add(appUserRole);
                dbModel.AppUsers.Add(appUserData);

                AddUpdateEntity(appUserRole);

                dbModel.SaveChanges();
                return appUserData.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }


        public bool ChangePassword(string loginid, string currentPassword, string newPassword)
        {
            try
            {
                string pwd = EncryptPassword(currentPassword);

                AppUser? user = dbModel.AppUsers.Where(x => x.LoginId.ToUpper() == loginid.ToUpper() && x.Password == pwd).FirstOrDefault();

                if (user == null)
                    return false;

                newPassword = EncryptPassword(newPassword);
                user.Password = newPassword;

                dbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool ResetPassword(string loginid, string newPassword)
        {
            try
            {
                AppUser? user = dbModel.AppUsers.Where(x => x.LoginId.ToUpper() == loginid.ToUpper()).FirstOrDefault();

                if (user == null)
                    return false;

                newPassword = EncryptPassword(newPassword);
                user.Password = newPassword;

                dbModel.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private static string EncryptPassword(string password)
        {
            byte[] userBytes = ASCIIEncoding.ASCII.GetBytes("CodeInDiary");
            string salt = Convert.ToBase64String(userBytes);

            string saltAndPwd = string.Concat(password, salt);

            UTF8Encoding encoder = new UTF8Encoding();
            using SHA256 sha256hasher = SHA256.Create();


            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));

            string hashedPwd = Convert.ToBase64String(hashedDataBytes);
            return hashedPwd;
        }



        public bool ResetPassword(string loginid)
        {
            try
            {
                AppUser? user = dbModel.AppUsers.Where(x => x.LoginId.ToUpper() == loginid.ToUpper()).FirstOrDefault();

                if (user == null)
                    return false;

                string newPassword = EncryptPassword("TulUser**");

                user.Password = newPassword;
                dbModel.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool CheckForExistingAppUserId(string loginId)
        {
            return GetEntityAny<AppUser>(x => x.LoginId.ToUpper() == loginId.ToUpper());
        }




        #region User Module

        public bool UpdateAppUserModule(AppUserModule userModule)
        {
            return AddUpdateEntity(userModule);
        }

        public int CreateAppUserModule(AppUserModule userModule)
        {
            AddUpdateEntity(userModule);
            return userModule.Id;
        }

        public AppUserModule GetAppUserModule(int userId, ModuleId moduleId)
        {
            return GetEntityFirstRowData<AppUserModule>(x => x.AppUserId == userId && x.ModuleId == moduleId);
        }

        public IList<AppUserModuleDTO> GetAppUserModules(int userId)
        {
            try
            {
                //var ss = Enum.GetValues(typeof(Roles));
                return (from UM in dbModel.AppUserModules
                        join R in dbModel.AppRoles on UM.AppRoleId equals R.Id
                        where UM.AppUserId == userId
                        select new AppUserModuleDTO
                        {
                            Id = UM.Id,
                            ModuleId = UM.ModuleId,
                            AppRoleId = R.Id,
                            AppUserId = UM.AppUserId,
                            HasAccess = UM.HasAccess,
                            RoleName = R.Description,
                            IsPrimary = false,
                        }).ToList()
                        .Union(from UR in dbModel.AppUserRoles
                               join R in dbModel.AppRoles on UR.AppRoleId equals R.Id
                               where UR.AppUserId == userId
                               select new AppUserModuleDTO
                               {
                                   AppRoleId = R.Id,
                                   AppUserId = UR.AppUserId,
                                   HasAccess = true,
                                   RoleName = R.Description,
                                   IsPrimary = true,
                               }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public IList<AppUserActivity> GetAppUserActivities()
        {
            return GetEntityListData<AppUserActivity>();
        }

        #endregion User Module

        #region User Restriction
        public int CreateUserRestriction(UserRestriction uerRestriction)
        {
            AddUpdateEntity(uerRestriction);
            return uerRestriction.Id;
        }

        public bool UpdateUserRestriction(UserRestriction uerRestriction)
        {
            return AddUpdateEntity(uerRestriction);
        }

        public UserRestriction GetUserRestriction(int id)
        {
            return FindEntity<UserRestriction>(id);
        }

        public IList<UserRestrictionDTO> GetAllUserRestrictionList()
        {
            return GetListData<UserRestrictionDTO>($"EXEC GetAllUserRestrictionList ");
        }

        #endregion User Restriction
        public IList<Lov> GetAllMigrationList()
        {
            return GetListData<Lov>($"SELECT Id = 1 ,Code = ProductVersion, Description = MigrationId FROM __EFMigrationsHistory");
        }
        #region Privileges

        public bool CheckUserPrivilege(int enumValue, int appUserId)
        {
            try
            {
                return dbModel.AppUserPrivileges.Where(x => x.Resource == (PrivilegedResource)enumValue && x.AppUserId == appUserId).Any();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<UserPrevilegesDTO> GetAppUserPrivileges(int userId)
        {
            try
            {
                return (from Rol in dbModel.AppUserRoles
                        join SPrvg in dbModel.SystemPrivileges on Rol.AppRoleId equals SPrvg.AppRoleId
                        join UPrvg in dbModel.AppUserPrivileges on new { SPrvg.Resource, Rol.AppUserId } equals new { UPrvg.Resource, UPrvg.AppUserId }
                        into temp
                        from result in temp.DefaultIfEmpty()
                        where Rol.AppUserId == userId
                        select new UserPrevilegesDTO
                        {
                            HasPrevilege = result == null ? false : true,
                            Resource = SPrvg.Resource
                        }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool UpdateAppUserPrivileges(ModuleId CurrentModule, int userId, IList<int> toAdd, IList<int> toDelete)
        {
            try
            {
                //Insert Newly Selected
                foreach (int resource in toAdd)
                {
                    dbModel.AppUserPrivileges.Add(new AppUserPrivilege
                    {
                        AppUserId = userId,
                        Resource = (PrivilegedResource)resource,
                        ModuleId = CurrentModule,
                    });
                }

                IList<AppUserPrivilege> ListToDelete = (dbModel.AppUserPrivileges
                                                        .Where(up => up.AppUserId == userId && up.ModuleId == CurrentModule && (toDelete.Cast<PrivilegedResource>()).Contains(up.Resource))
                                                        ).ToList();

                dbModel.AppUserPrivileges.RemoveRange(ListToDelete);
                dbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public IList<PrivilegedResource> GetAuthorizedCaseReports(int userId)
        {
            try
            {
                List<PrivilegedResource> reportsList = new List<PrivilegedResource>
                {
                    //PrivilegedResource.InterviewNotesReport,
                };

                return (from P in dbModel.AppUserPrivileges
                        where P.AppUserId == userId
                        && ((reportsList).Contains(P.Resource))
                        select P.Resource).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public IList<PrivilegedResource> GetAuthorizedCannedReports(int userId)
        {
            try
            {
                List<PrivilegedResource> reportsList = new List<PrivilegedResource>
                {
                    //PrivilegedResource.CaseUnderInvestigatorReport,
                };

                return (from P in dbModel.AppUserPrivileges
                        where P.AppUserId == userId
                        && ((reportsList).Contains(P.Resource))
                        select P.Resource).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public IList<PrivilegedResource> GetAuthorizedAdhocReports(int userId)
        {
            try
            {
                List<PrivilegedResource> reportsList = new List<PrivilegedResource>
                {
                    //PrivilegedResource.ReferralsSummaryReport,
                };

                return (from P in dbModel.AppUserPrivileges
                        where P.AppUserId == userId
                        && ((reportsList).Contains(P.Resource))
                        select P.Resource).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool ResetUserPrivileges(int appUserId, ModuleId moduleId, bool isPrimary, List<PrivilegedResource> toDelete)
        {
            try
            {
                int roleId = 0;

                if (isPrimary)
                {
                    roleId = dbModel.AppUserRoles.FirstOrDefault(x => x.AppUserId == appUserId).AppRoleId;
                }
                else
                {
                    roleId = dbModel.AppUserModules.FirstOrDefault(x => x.AppUserId == appUserId && x.ModuleId == moduleId).AppRoleId;
                }

                var tox = dbModel.AppUserPrivileges.Where(c => c.AppUserId == appUserId && c.ModuleId == moduleId && toDelete.Contains(c.Resource));
                dbModel.AppUserPrivileges.RemoveRange(dbModel.AppUserPrivileges.Where(c => c.AppUserId == appUserId && c.ModuleId == moduleId && toDelete.Contains(c.Resource)));

                dbModel.AppUserPrivileges.AddRange(
                    from SP in dbModel.SystemPrivileges
                    where SP.AppRoleId == roleId
                    select new AppUserPrivilege
                    {
                        AppUserId = appUserId,
                        Resource = SP.Resource,
                        ModuleId = moduleId,
                    });

                dbModel.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<int> GetAppRolePrivileges(int roleId)
        {
            try
            {
                return (from P in dbModel.SystemPrivileges
                        where P.AppRoleId == roleId
                        select (int)P.Resource).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool UpdateAppRolePrivileges(int roleId, IList<int> toAdd, IList<int> toDelete)
        {
            try
            {
                IList<int> users = (from U in dbModel.AppUserRoles where U.AppRoleId == roleId select U.AppUserId).ToList();

                //Add the New Privileges to the Role
                dbModel.SystemPrivileges.AddRange(
                    from list in toAdd
                    select new SystemPrivilege { AppRoleId = roleId, Resource = (PrivilegedResource)list });

                //Remove the Deleted Privileges from the Role
                dbModel.SystemPrivileges.RemoveRange(dbModel.SystemPrivileges.Where(sp => sp.AppRoleId == roleId && (toDelete.Cast<PrivilegedResource>()).Contains(sp.Resource)));

                //Remove the Deleted Privileges from Users Under the Role
                dbModel.AppUserPrivileges.RemoveRange(
                    dbModel.AppUserPrivileges.
                    Where(up => users.Contains(up.AppUserId) &&
                    (toDelete.Cast<PrivilegedResource>()).Contains(up.Resource))
                    );

                dbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<UserPrevilegesDTO> GetAppUserPrivilegesByPrimaryRole(int userId)
        {
            try
            {
                return GetListData<UserPrevilegesDTO>($"EXEC GetAppUserPrivilegesByPrimaryRole @userId={userId}").ToList();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }



        #endregion Privileges
    }
}