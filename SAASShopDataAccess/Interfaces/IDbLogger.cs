namespace SAASShopDataAccess
{
    public interface IDbLogger
    {
        bool CriticalEntry(string user, string errMessage);
    }
}