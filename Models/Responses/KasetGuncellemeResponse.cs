using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Responses
{
    public class KasetGuncellemeResponse
    {
        public bool IslemBasariliMi { get; set; }
        public string HataKodu { get; set; } = "Hata Kodu Yok";
    }
}