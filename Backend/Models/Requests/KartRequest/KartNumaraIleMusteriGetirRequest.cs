// Models/Dtos/Requests/Kart/KartNumaraIleMusteriIdGetirRequest.cs
using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.Kart
{
    public class KartNumaraIleMusteriIdGetirRequest
    {
        [Required(ErrorMessage = "Kart numarası zorunludur")]
        public string KartNumara { get; set; }
    }
}