using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class IslemGecmisi
    {
        [Key]
        public int Id { get; set; }
        public string? HesapNumara { get; set; } //Kartsız işlemler olabilir bu sebeple nullable
        public string? KartsiTarafHesapNumara { get; set; } //Tek yönlü bir işlem olabilir bu sebeple nullable
        public string IslemTuru { get; set; } = string.Empty;
        public string IslemYonu { get; set; } = string.Empty;
        public DateTime IslemTarihi { get; set; }
        public string? IslemAciklama { get; set; } //Açıklamasız işlem olabilir bu sebeple nullable
        public decimal IslemTutar { get; set; }
        public decimal IslemSonrasiBakiye { get; set; }
        public int AtmID { get; set; }
    }
}