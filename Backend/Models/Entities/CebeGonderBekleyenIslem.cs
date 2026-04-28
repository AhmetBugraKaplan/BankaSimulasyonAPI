using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    [Table("CebeGonderBekleyenIslemler")]
    public class CebeGonderBekleyenIslem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(16)]
        public string GonderenHesapNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(11)]
        public string AliciTckNO { get; set; } = string.Empty;

        [Required]
        [MaxLength(16)]
        public string AliciTelNo { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tutar { get; set; }

        public DateTime GonderimTarihi { get; set; }

        public DateTime SonKabullenmeTarihi { get; set; }

        [Required]
        [MaxLength(20)]
        public string Durum { get; set; } = string.Empty;// Bekliyor | Cekildi | IadeEdildi

        public DateTime? CekilmeTarihi { get; set; }

    }
}