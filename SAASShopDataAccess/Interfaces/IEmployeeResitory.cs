using SAASShopDataAccess.DbModel;
using SAASShopDomain;
using SAASShopDomain.User;
namespace SAASShopDataAccess.Interfaces
{
    public interface IEmployeeResitory
    {
        
        // RegisterViewModel GetEditRegister(int LoginId);
        
        RegisterViewModel UpdateRegister(RegisterViewModel UpdateEmployee);
        RegisterViewModel Delete(int LoginId);

    }
}
