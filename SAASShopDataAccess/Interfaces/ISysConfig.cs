using SAASShopDomain;

namespace SAASShopDataAccess
{
    public interface ISysConfig
    {
        IList<SystemConfiguration> GetSystemConfigurationList();

        bool UpdateConfiguration(SystemConfiguration configData);

        SystemConfiguration GetSystemConfiguration(ConfigurationCode code);
    }
}