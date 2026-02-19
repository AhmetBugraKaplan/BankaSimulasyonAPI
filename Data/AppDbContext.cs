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
        public DbSet<Kullanici> Kullanicilar { get; set; } = null!;
        public DbSet<KullaniciHesap> KullaniciHesaplari { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ATM>().HasData(
                new ATM { Id = 1, Konum = "Zeytinburnu Beştelsiz Şube", AktifMi = true },
                new ATM { Id = 2, Konum = "Bakırköy Cadde Şube", AktifMi = false }
            );

            modelBuilder.Entity<AtmKaset>().HasData(
                new AtmKaset { Id = 1, AtmId = 1, SlotNumarasi = 1, Kupur = 200, Adet = 20, KritikDeger = 20 },
                new AtmKaset { Id = 2, AtmId = 1, SlotNumarasi = 2, Kupur = 100, Adet = 20, KritikDeger = 20 },
                new AtmKaset { Id = 3, AtmId = 1, SlotNumarasi = 3, Kupur = 50, Adet = 20, KritikDeger = 20 },
                new AtmKaset { Id = 4, AtmId = 1, SlotNumarasi = 4, Kupur = 20, Adet = 20, KritikDeger = 20 }
            );

            modelBuilder.Entity<Kullanici>().HasData(
                new Kullanici {id = 1,Isim = "BugraTest",Soyisim = "Kaplan",Adres="Zeytinburnu"}
            );

            modelBuilder.Entity<KullaniciHesap>().HasData(
                new KullaniciHesap {id=1,KullaniciId=1,HesapNumarasi= 000 , Bakiye = 100000}
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}