using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BankaSimulasyon.Models.Requests
{
    public class KartDogrulaRequest
    {
        [Required(ErrorMessage = "Kart numarası zorunludur")]
        public string KartNumara { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [Length(3,3)]
        public string KartSifre { get; set; } = string.Empty;

        [Required(ErrorMessage = "ATM ID zorunludur")]
        [Range(1, int.MaxValue)]
        public int AtmId { get; set; }
    }
}
