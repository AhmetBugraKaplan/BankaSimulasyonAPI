using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class HesabaKartsizParaGonderRequest
{
    public string HesapNumara { get; set; } = string.Empty;
    public decimal GonderilecekTutar { get; set; }
}
}