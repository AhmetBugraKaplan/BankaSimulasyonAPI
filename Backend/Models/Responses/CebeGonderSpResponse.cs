using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Responses
{
    public class CebeGonderSpResponse
    {
        public int Sonuc { get; set; }
        public string Mesaj { get; set; } = string.Empty;
    }
}