using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class AtmdeNeKadarParaVarRequest
    {
        [Required(ErrorMessage="ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage="Gecerli bir ID giriniz")]
        public int AtmId { get; set; }
    }
}


