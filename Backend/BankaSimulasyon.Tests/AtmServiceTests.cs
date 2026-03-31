using BankaSimulasyon.Models.Entities;
using BankaSimulasyon.Repositories;
using BankaSimulasyon.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;




namespace BankaSimulasyon.Tests
{
    public class AtmServiceTests
    {
        private readonly Mock<IAtmKasetRepository> _mockAtmKasetRepository;
        private readonly Mock<IAtmRepository> _mockAtmRepository;        // yeni ekle
        private readonly AtmService _atmService;

        public AtmServiceTests()
        {
            _mockAtmKasetRepository = new Mock<IAtmKasetRepository>();
            _mockAtmRepository = new Mock<IAtmRepository>();             // yeni ekle
            _atmService = new AtmService(_mockAtmKasetRepository.Object, _mockAtmRepository.Object);  // ikisini de ver
        }


        [Fact]
        public async Task AtmdenParaCekAsync_YeterliBakiyeVeUygunKasetler_BasariliDonmeli()
        {
            // 1. ARRANGE
            int testAtmId = 1;
            int cekilecekTutar = 250;

            var sahteKasetler = GetOrnekKasetler(testAtmId);

            _mockAtmKasetRepository
                .Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId))
                .ReturnsAsync(sahteKasetler);

            // 2. ACT
            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            // 3. ASSERT
            Assert.True(sonuc.IslemBasariliMi);
            Assert.Equal("Para basariyla cekildi", sonuc.Mesaj);
            Assert.NotNull(sonuc.Kasetler);

            // 200 TL x 1 ve 50 TL x 1 verilmeli (Algoritma en büyükten başlar normalde)
            // Ama kodda kritik değer kontrolü var. Kritik değer = 5, Stok = 10. Stok > Kritik.
            // O yüzden normal dağıtım yapacak.

            _mockAtmKasetRepository.Verify(repo => repo.AtmKasetGuncelleAsync(It.IsAny<AtmKaset>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task AtmdenParaCekAsync_YetersizBakiye_BasarisizDonmeli()
        {
            // 1. ARRANGE
            int testAtmId = 1;
            int cekilecekTutar = 5000; // Toplamda 3600 var

            var sahteKasetler = GetOrnekKasetler(testAtmId);

            _mockAtmKasetRepository
                .Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId))
                .ReturnsAsync(sahteKasetler);

            // 2. ACT
            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            // 3. ASSERT
            Assert.False(sonuc.IslemBasariliMi);
            Assert.Equal("ATM'de yeterli para bulunmuyor", sonuc.Mesaj);
            _mockAtmKasetRepository.Verify(repo => repo.AtmKasetGuncelleAsync(It.IsAny<AtmKaset>()), Times.Never);
        }

        [Fact]
        public async Task AtmdenParaCekAsync_GecersizTutar_10unKatiDegilse_HataDonmeli()
        {
            // 1. ARRANGE
            int testAtmId = 1;
            int cekilecekTutar = 125; // 10'a bölünmez

            var sahteKasetler = GetOrnekKasetler(testAtmId);

            _mockAtmKasetRepository.Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId)).ReturnsAsync(sahteKasetler);

            // 2. ACT
            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            // 3. ASSERT
            Assert.False(sonuc.IslemBasariliMi);
            Assert.Equal("Cekilmek istenen para 10TL'nin katlari olmalidir", sonuc.Mesaj);
        }

        [Fact]
        public async Task AtmdenParaCekAsync_KritikSeviyeAltindakiKaseti_PasGecmeli()
        {
            // 1. ARRANGE
            int testAtmId = 1;
            int cekilecekTutar = 200;

            // Manipüle edilmiş kaset listesi:
            // 200 TL'lik kasetin adedi kritik seviyenin (5) altında olsun. Örn: 4 tane var.
            var sahteKasetler = new List<AtmKaset>
            {
                new AtmKaset { Id = 1, AtmId = testAtmId, SlotNumarasi = 1, Kupur = 200, Adet = 4, KritikDeger = 5 }, // KRİTİK! Vermemeli.
                new AtmKaset { Id = 2, AtmId = testAtmId, SlotNumarasi = 2, Kupur = 100, Adet = 10, KritikDeger = 5 }, // 100 TL verir
                new AtmKaset { Id = 3, AtmId = testAtmId, SlotNumarasi = 3, Kupur = 50,  Adet = 10, KritikDeger = 5 },
                new AtmKaset { Id = 4, AtmId = testAtmId, SlotNumarasi = 4, Kupur = 10,  Adet = 10, KritikDeger = 5 }
            };

            _mockAtmKasetRepository.Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId)).ReturnsAsync(sahteKasetler);

            // 2. ACT
            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            // 3. ASSERT
            Assert.True(sonuc.IslemBasariliMi);

            // Normalde 1 tane 200 TL verip işi bitirirdi.
            // Ama 200 TL kritik seviyede olduğu için, bir sonraki uygun kupure (100 TL) geçer.
            // 2 tane 100 TL vermesini bekliyoruz.

            var kullanilan200luk = sonuc.Kasetler.FirstOrDefault(k => k.Kupur == 200);
            var kullanilan100luk = sonuc.Kasetler.FirstOrDefault(k => k.Kupur == 100);

            // 200'lük hiç kullanılmamış olmalı (veya listede hiç olmamalı)
            Assert.True(kullanilan200luk == null || kullanilan200luk.Adet == 0);

            // 100'lükten 2 tane kullanılmalı
            Assert.NotNull(kullanilan100luk);
            Assert.Equal(2, kullanilan100luk.Adet);
        }

        [Fact]
        public async Task AtmdenParaCekAsync_ParaBozmaSenaryosu_Calismali()
        {
            // 1. ARRANGE
            int testAtmId = 1;

            // Senaryo: 20 TL çekmek istiyoruz.
            // Kasetler: 200(10), 100(10), 20(1), 10(100)
            // Normal döngü: İlk başta 20'yi bulur verir.
            // Ama biz "Para Bozma"yı tetiklemek için şöyle bir durum yaratalım:
            // 60 TL çekmek isteyelim.
            // Kasetler: 50 TL (1 tane, kritik değil), 20 TL (0 tane), 10 TL (0 tane). -> Olmaz, yetersiz bakiye.

            // Bozma algoritmasını tetiklemek için:
            // "toplamVerilenBanknot >= bozmaEsigi (3)" olmalı ve sonKullanilanKaset != null olmalı.
            // Ve cekilecekTutar == 0 olmalı.

            // ATMService.cs satır 78: 
            // if (cekilecekTutar == 0 && sonKullanilanKaset != null && toplamVerilenBanknot >= bozmaEsigi)

            // 40 TL çekmek isteyelim.
            // Kasetler: 10 TL'likten bolca var.
            // Algoritma: 30 TL verirse (3 banknot) -> Eşik sağlanır.
            // Ama bizim algoritma en büyükten küçüğe gidiyor.

            // Test Edilecek Senaryo: 300 TL çekelim.
            // Kasetler: 100 TL (3 tane verecek -> Eşik 3).
            // Kasetlerde ayrıca 50 TL de olsun.
            // Algoritma: 3 tane 100 TL ayarladı. Eşik aşıldı (>=3).
            // 1 tane 100 TL'yi geri koyup (Elde 2x100 kaldı), o 100 TL'yi 50'liklerle tamamlamaya çalışmalı.
            // Sonuç: 2 adet 100 TL, 2 adet 50 TL olmalı.

            int cekilecekTutar = 300;
            var sahteKasetler = new List<AtmKaset>
            {
                new AtmKaset { Id = 1, AtmId = testAtmId, SlotNumarasi = 1, Kupur = 200, Adet = 0, KritikDeger = 5 },
                new AtmKaset { Id = 2, AtmId = testAtmId, SlotNumarasi = 2, Kupur = 100, Adet = 10, KritikDeger = 5 }, // 100 TL - Stok var
                new AtmKaset { Id = 3, AtmId = testAtmId, SlotNumarasi = 3, Kupur = 50,  Adet = 10, KritikDeger = 5 }, // 50 TL - Stok var
                new AtmKaset { Id = 4, AtmId = testAtmId, SlotNumarasi = 4, Kupur = 10,  Adet = 10, KritikDeger = 5 }
            };

            _mockAtmKasetRepository.Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId)).ReturnsAsync(sahteKasetler);

            // 2. ACT
            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            // 3. ASSERT
            Assert.True(sonuc.IslemBasariliMi);

            var kaset100 = sonuc.Kasetler.FirstOrDefault(k => k.Kupur == 100);
            var kaset50 = sonuc.Kasetler.FirstOrDefault(k => k.Kupur == 50);

            // Normalde 3 tane 100 verirdi. Ama 3 adet >= 3 eşiği olduğu için 1 tanesini bozmalı.
            // Beklenen: 2 tane 100, 2 tane 50.
            Assert.NotNull(kaset100);
            Assert.Equal(2, kaset100.Adet);

            Assert.NotNull(kaset50);
            Assert.Equal(2, kaset50.Adet);
        }


        [Fact]
        public async Task AtmdenParaCekAsync_KupurlerUyusmuyor_BasarisizDonmeli()
        {
            int testAtmId = 1;
            int cekilecekTutar = 30;

            var sahteKasetler = new List<AtmKaset>
    {
        new AtmKaset { Id = 1, AtmId = testAtmId, SlotNumarasi = 1, Kupur = 200, Adet = 10, KritikDeger = 5 },
        new AtmKaset { Id = 2, AtmId = testAtmId, SlotNumarasi = 2, Kupur = 50, Adet = 10, KritikDeger = 5 }
    };

            _mockAtmKasetRepository.Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId)).ReturnsAsync(sahteKasetler);

            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            Assert.False(sonuc.IslemBasariliMi);
            Assert.Equal("Kupurler uyusmuyor, islem gerceklestirilemedi", sonuc.Mesaj);
            _mockAtmKasetRepository.Verify(repo => repo.AtmKasetGuncelleAsync(It.IsAny<AtmKaset>()), Times.Never);
        }


        [Fact]
        public async Task AtmdenParaCekAsync_SifirTutar_BasarisizDonmeli()
        {
            int testAtmId = 1;
            int cekilecekTutar = 0;

            var sahteKasetler = GetOrnekKasetler(testAtmId);
            _mockAtmKasetRepository.Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId)).ReturnsAsync(sahteKasetler);

            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            Assert.False(sonuc.IslemBasariliMi);
            _mockAtmKasetRepository.Verify(repo => repo.AtmKasetGuncelleAsync(It.IsAny<AtmKaset>()), Times.Never);
        }


        [Fact]
        public async Task AtmdenParaCekAsync_TumKasetlerKritik_IkinciGecisteVerilmeli()
        {
            int testAtmId = 1;
            int cekilecekTutar = 80;

            var sahteKasetler = new List<AtmKaset>
    {
        new AtmKaset { Id = 1, AtmId = testAtmId, SlotNumarasi = 1, Kupur = 200, Adet = 3, KritikDeger = 5 },
        new AtmKaset { Id = 2, AtmId = testAtmId, SlotNumarasi = 2, Kupur = 100, Adet = 3, KritikDeger = 5 },
        new AtmKaset { Id = 3, AtmId = testAtmId, SlotNumarasi = 3, Kupur = 50, Adet = 3, KritikDeger = 5 },
        new AtmKaset { Id = 4, AtmId = testAtmId, SlotNumarasi = 4, Kupur = 10, Adet = 3, KritikDeger = 5 }
    };

            _mockAtmKasetRepository.Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId)).ReturnsAsync(sahteKasetler);

            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            Assert.True(sonuc.IslemBasariliMi);
            Assert.Equal("Para basariyla cekildi", sonuc.Mesaj);
            _mockAtmKasetRepository.Verify(repo => repo.AtmKasetGuncelleAsync(It.IsAny<AtmKaset>()), Times.AtLeastOnce);
        }


        [Fact]
        public async Task AtmdenParaCekAsync_BosAtm_BasarisizDonmeli()
        {
            int testAtmId = 99;
            int cekilecekTutar = 80;

            _mockAtmKasetRepository.Setup(repo => repo.AtmdekiKasetleriGetirAsync(testAtmId)).ReturnsAsync(new List<AtmKaset>());

            var sonuc = await _atmService.AtmdenParaCekAsync(testAtmId, cekilecekTutar);

            Assert.False(sonuc.IslemBasariliMi);
            _mockAtmKasetRepository.Verify(repo => repo.AtmKasetGuncelleAsync(It.IsAny<AtmKaset>()), Times.Never);
        }


























        // Yardımcı Metot: Her testte tekrar tekrar liste oluşturmamak için
        private List<AtmKaset> GetOrnekKasetler(int atmId)
        {
            return new List<AtmKaset>
            {
                new AtmKaset { Id = 1, AtmId = atmId, SlotNumarasi = 1, Kupur = 200, Adet = 10, KritikDeger = 5 }, // 2000 TL
                new AtmKaset { Id = 2, AtmId = atmId, SlotNumarasi = 2, Kupur = 100, Adet = 10, KritikDeger = 5 }, // 1000 TL
                new AtmKaset { Id = 3, AtmId = atmId, SlotNumarasi = 3, Kupur = 50,  Adet = 10, KritikDeger = 5 }, // 500 TL
                new AtmKaset { Id = 4, AtmId = atmId, SlotNumarasi = 4, Kupur = 10,  Adet = 10, KritikDeger = 5 }  // 100 TL
            };
        }
    }
}