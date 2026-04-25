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


        public SmsService(IConfiguration config, ILogger<SmsService> logger, IOnayRepository onayRepository)
        {
            _config = config;
            _logger = logger;
            _onayRepository = onayRepository;
        }

        public void SmsGonder(string telefonNumara, string kod)
        {
            _logger.LogWarning(">>> OTP KOD: {Kod} | Telefon: {Telefon}", kod, telefonNumara);
            _onayRepository.OnayKodunuDbKaydet(kod, telefonNumara);
        }
    }
}



