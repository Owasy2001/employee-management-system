using SAASShopDataAccess.DbModel;
using SAASShopDomain;

namespace SAASShopDataAccess
{
    public class DbLogManager : BaseDataManager, IDbLogger
    {
        public DbLogManager(SAASShopModel context) : base(context)
        {
        }

        public bool CriticalEntry(string user, string errMessage)
        {
            ErrorLog eLog = new ErrorLog()
            {
                Error = errMessage,
                LoggedBy = user,
                LoggedOn = DateTime.Now
            };

            return AddUpdateEntity<ErrorLog>(eLog);
        }
    }
}