using SAASShopDataAccess.DbModel;
using SAASShopDomain;

namespace SAASShopDataAccess
{
    public class SAASShopManager : BaseDataManager, ISAASShop
    {
        public SAASShopManager(SAASShopModel context) : base(context)
        {
        }

        public SAASShopProduct GetProductInfo()
        {
            return GetEntityListData<SAASShopProduct>().FirstOrDefault();
        }
    }
}