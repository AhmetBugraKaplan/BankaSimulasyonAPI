using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class MusteriTumHesaplariGetirRequest
    {
        [Required(ErrorMessage = "Müşteri ID zorunludur")]
        public string KartNumara { get; set; } = string.Empty;
    }
}