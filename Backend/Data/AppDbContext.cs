using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankaSimulasyon.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ATM> AtmLer { get; set; } = null!;
        public DbSet<AtmKaset> AtmKasetler { get; set; } = null!;
        public DbSet<Musteri> Musteriler { get; set; } = null!;
        public DbSet<Kart> Kartlar { get; set; } = null!;
        public DbSet<KartSifre> KartSifreleri { get; set; } = null!;
        public DbSet<KartLimit> KartLimitleri { get; set; } = null!;
        public DbSet<Hesap> Hesaplar { get; set; } = null!;
        public DbSet<OnayKod> OnayKodlari { get; set; } = null!;
        public DbSet<CebeGonderBekleyenIslem> CebeGonderBekleyenIslemler { get; set; } = null!;


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    }

}
