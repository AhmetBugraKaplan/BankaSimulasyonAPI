using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BankaSimulasyon.Services
{
    public interface ISmsService
    {
        void SmsGonder(string telefonNumara, string kod);
    }

    public class SmsService : ISmsService
    {
        public void SmsGonder(string telefonNumara, string kod)
        {
            TwilioClient.Init("", "");

            MessageResource.Create(
                to: new Twilio.Types.PhoneNumber("+90" + telefonNumara),
                from: new Twilio.Types.PhoneNumber(""),
                body: $"ATM doğrulama kodunuz: {kod}"
            );
        }
    }
}