using BankaSimulasyon.Repositories;
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
        private readonly IConfiguration _config;
        private readonly ILogger<SmsService> _logger;
        private readonly IOnayRepository _onayRepository;
        

        public SmsService(IConfiguration config, ILogger<SmsService> logger,IOnayRepository onayRepository)
        {
            _config = config;
            _logger = logger;
            _onayRepository = onayRepository;
        }

        public void SmsGonder(string telefonNumara, string kod)
        {

            TwilioClient.Init("", "");

            _logger.LogWarning(">>> OTP KOD: {Kod} | Telefon: {Telefon}", kod, telefonNumara);


            var accountSid = _config["Twilio:AccountSid"];
            var authToken = _config["Twilio:AuthToken"];
            var fromNumber = _config["Twilio:FromNumber"];

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new Twilio.Types.PhoneNumber("+90" + telefonNumara),

                from: new Twilio.Types.PhoneNumber(""),


                body: $"ATM doğrulama kodunuz: {kod}"
            );

            _logger.LogWarning(">>> Twilio Status: {Status} | SID: {Sid}", message.Status, message.Sid);

            _onayRepository.OnayKoduUret(kod,telefonNumara);
        }
    }
}



