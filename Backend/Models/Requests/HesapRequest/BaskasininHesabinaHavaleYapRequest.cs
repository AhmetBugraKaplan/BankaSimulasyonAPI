using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class BaskasininHesabinaHavaleYapRequest
    {
        [Required(ErrorMessage = "Gönderen hesap numarası zorunludur")]
        public string GonderenHesapNumara { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Alıcı hesap numarası zorunludur")]
        public string AliciHesapNumara { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gönderilecek tutar zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır")]
        public decimal GonderilenTutar { get; set;}

        [Required(ErrorMessage = "Kart numarası zorunludur")]
        public string KartNumara { get; set; } = string.Empty;
    }
}