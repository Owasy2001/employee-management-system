using Azure.Communication.Sms;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SAASShop.Communications
{
    public class ComServiceManager : IComService
    {
        public async Task<bool> SendSecurityCodeByEmail(string toEmail, string securityCode, string authKey)
        {
            try
            {
                var client = new SendGridClient(authKey);

                string emailSubject = "";
                string emailBody = $"Code In Diary Security Code to Verify Your Access{securityCode}";

                var mailMsg = new SendGridMessage();
                mailMsg.SetFrom(new EmailAddress("kslimon41@gmail.com", name: "Code In Diary"));

                mailMsg.AddTo(new EmailAddress(toEmail));
                mailMsg.SetSubject(emailSubject);


                mailMsg.AddContent(MimeType.Text, emailBody);

                var response = await client.SendEmailAsync(mailMsg);
                return response.IsSuccessStatusCode;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> SendSecurityCodeByText(string fromPhone, string toPhone, string securityCode, string authKey)
        {
            try
            {
                string smsMsg = $"Code In Diary Security Code to Verify Your Access {securityCode}";

                SmsClient smsClient = new(authKey);
                SmsSendResult ssResult = await smsClient.SendAsync(from: fromPhone, to: toPhone, message: smsMsg);

                return ssResult.Successful;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
