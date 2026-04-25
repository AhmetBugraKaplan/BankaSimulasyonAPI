using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class CebeParaGonderRequest
    {
        public string aliciTckNO { get; set; } = string.Empty;
        public string aliciTelNo { get; set; } = string.Empty;
        public decimal gonderilenTutar { get; set; } = 0;
    }
}