
using SAASShopDataAccess.DbModel;
using SAASShopDomain;

namespace SAASShopDataAccess
{
    public class SysConfigManager : BaseDataManager, ISysConfig
    {
        public SysConfigManager(SAASShopModel context) : base(context)
        {
        }

        public IList<SystemConfiguration> GetSystemConfigurationList()
        {
            return GetEntityListData<SystemConfiguration>();
        }

        public bool UpdateConfiguration(SystemConfiguration configData)
        {
            return AddUpdateEntity(configData);
        }

        public SystemConfiguration GetSystemConfiguration(ConfigurationCode code)
        {
            return GetEntityFirstRowData<SystemConfiguration>(x => x.Code == code && x.IsActive);
        }
    }
}