using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Responses
{
    public class ApiResponse
    {
        public bool IslemBasariliMi { get; set; }
        public string Mesaj { get; set; } = "Hata Yok";
    }
}