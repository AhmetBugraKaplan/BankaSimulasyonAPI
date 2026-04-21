using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.OnayRequest
{
    public class OnayKoduDogruMuRequest
    {
        public string Kod { get; set; } = string.Empty;
        public string TelefonNumara { get; set; } = string.Empty;
    }
}