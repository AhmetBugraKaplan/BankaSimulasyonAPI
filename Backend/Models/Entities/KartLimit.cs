using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    [Table("KartLimitleri")]
    public class KartLimit
    {
        [Key]
        public int Id { get; set; }
        public decimal KartGunlukLimit { get; set; } = 0;
        public decimal KartKalanLimit { get; set; } = 0;
        //FK
        public int KartId { get; set; }
        public Kart Kart { get; set; } = null!;
    }
}