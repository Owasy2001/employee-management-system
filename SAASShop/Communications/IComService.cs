namespace SAASShop.Communications
{
    public interface IComService
    {
        Task<bool> SendSecurityCodeByEmail(string toEmail, string securityCode, string authKey);
        Task<bool> SendSecurityCodeByText(string fromPhone, string toPhone, string securityCode, string authKey);
    }
}
