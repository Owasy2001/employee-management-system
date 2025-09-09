using Microsoft.AspNetCore.Mvc;
using SAASShop.Communications;
using SAASShopDataAccess;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAASShop.Pages.Security
{
    public class MFAOptionModel : TransactBasePage
    {
        [BindProperty]
        [Required(ErrorMessage = "Verification Code is required")]
        public ComType? Selectiontype { get; set; }

        [BindProperty]
        public string MaskedEmail { get; set; }

        [BindProperty]
        public string MaskedPhoneNumber { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        private readonly ISecurity m_UserSecurity;
        private readonly IDbLogger m_Log;
        private readonly IConfiguration m_Configuration;
        private readonly IComService m_ComService;

        public MFAOptionModel(ISecurity security, IDbLogger logger, IConfiguration configuration, IComService comService)
        {
            m_UserSecurity = security;
            m_Log = logger;
            m_Configuration = configuration;
            m_ComService = comService;
        }

        public void OnGet()
        {
            var user = m_UserSecurity.GetUserByLoginId(HttpContext.Session.GetString("LoginID"));
            StoreDeliveryData(user.Email, user.Phone);
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                Random generator = new();
                string code = generator.Next(0, 1000000).ToString("D6");

                HttpContext.Session.SetString("SecurityCode", code);
                bool tokenSent = false;

                if (Selectiontype == ComType.Email)
                {
                    string emailKey = m_Configuration.GetValue<string>("MFAString:EmailKey");
                    tokenSent = await m_ComService.SendSecurityCodeByEmail(Email, code, emailKey);


                }
                else if (Selectiontype == ComType.Text)
                {
                    string authKey = m_Configuration.GetValue<string>("MFAString:AccountKey");
                    string authPhone = m_Configuration.GetValue<string>("MFAString:AccountPhone");

                    tokenSent = await m_ComService.SendSecurityCodeByText(authPhone, PhoneNumber, code, authKey);
                }

                if (tokenSent)
                {
                    return RedirectToPage(Navigator.VerifyMFA);
                }
                else
                {
                    TempData["Message"] = "There was an issue sending the security code. Please try again.";
                    return RedirectToPage(Navigator.MFAOption);
                }
            }
            catch (Exception ex)
            {
                m_Log.CriticalEntry(Email, ex.ToString());
                return RedirectToPage(Navigator.Login);
            }
        }

        private void StoreDeliveryData(string emailId, string phoneNo)
        {
            if (!string.IsNullOrEmpty(emailId))
            {
                string email = emailId;
                var firstDigits = email[..4];
                var requiredMask = new string('*', email.Length - 10);
                MaskedEmail = string.Concat(firstDigits, requiredMask);
                Email = emailId;
            }

            if (!string.IsNullOrEmpty(phoneNo))
            {
                string phoneNumber = phoneNo;
                MaskedPhoneNumber = string.Concat(new string('*', phoneNumber.Length - 4), phoneNumber[^4..]);
                PhoneNumber = "+" + phoneNo;
            }
        }
    }

    public enum ComType
    {
        Email = 1,
        Text = 2
    }
}

