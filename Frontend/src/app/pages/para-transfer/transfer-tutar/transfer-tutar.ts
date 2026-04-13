import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { forkJoin, timer } from 'rxjs';
import { Hesap, HesapService, HavaleTalebi } from '../../../services/hesap';

@Component({
  selector: 'app-transfer-tutar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './transfer-tutar.html',
  styleUrl: './transfer-tutar.css',
})
export class TransferTutar implements OnInit {
  secilenHesap: Hesap | null = null;
  aliciHesapNumara: string = '';
  tutar: number | null = null;

  // Sadece bu 3 durum değişkeni bize yeter
  yukleniyor: boolean = false;
  mesaj: string = '';
  islemBasariliMi: boolean = false;

  constructor(
    private router: Router,
    private hesapService: HesapService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    const hesapData = sessionStorage.getItem('secilenHesap');
    const aliciData = sessionStorage.getItem('aliciHesapNumara');

    if (!hesapData || !aliciData) {
      this.router.navigate(['/hesap-sec']);
      return;
    }

    this.secilenHesap = JSON.parse(hesapData);
    this.aliciHesapNumara = aliciData;
  }

  devam(): void {
    // 1. Ekranı temizle
    this.mesaj = '';

    // 2. Frontend Validasyonları
    if (!this.tutar || this.tutar <= 0) {
      this.mesaj = 'Lütfen sıfırdan büyük geçerli bir tutar giriniz.';
      return;
    }

    if (this.secilenHesap && this.tutar > this.secilenHesap.hesapBakiye) {
      this.mesaj = 'Yetersiz bakiye. Girdiğiniz tutar hesabınızdaki parayı aşıyor.';
      return;
    }


    // 3. Yükleniyor Animasyonunu Başlat
    this.yukleniyor = true;
    this.cdr.detectChanges();

    const kartNo = sessionStorage.getItem('kartNumara') || '';
    const talep: HavaleTalebi = {
      GonderenHesapNumara: this.secilenHesap!.hesapNumara,
      AliciHesapNumara: this.aliciHesapNumara,
      GonderilenTutar: this.tutar,
      KartNumara: kartNo
    };

    // 4. İsteği Gönder
    forkJoin({
      apiCevabi: this.hesapService.havaleYap(talep),
      beklemeSuresi: timer(1000) // Minimum 1000 milisaniye (1 saniye) spinner dönecek
    }).subscribe({
      next: (sonuc) => {
        // sonuc.apiCevabi içinde backend'den dönen asıl response var
        const res = sonuc.apiCevabi; 
        
        this.yukleniyor = false; // Süre doldu, Spinner'ı KAPAT
        this.cdr.detectChanges();
        
        if (res.islemBasariliMi) {
          // Başarılıysa hiç mesaj göstermeden doğrudan dekont/onay sayfasına uçur!
          sessionStorage.removeItem('secilenHesap');
          sessionStorage.removeItem('aliciHesapNumara');
          this.router.navigate(['/islem-onaylandi']); 
        } else {
          // Eğer SP'den veya servisten hata dönerse ekranda göster
          this.mesaj = res.mesaj || 'İşlem sırasında bir hata oluştu.';
        }
      },
      error: (err: any) => {
        this.yukleniyor = false;
        if (err.error && err.error.errors) {
            const ilkHataAnahtari = Object.keys(err.error.errors)[0];
            this.mesaj = err.error.errors[ilkHataAnahtari][0];
        } else if (err.error && typeof err.error === 'string') {
            this.mesaj = err.error;
        } else {
            this.mesaj = 'Sunucu ile iletişim kurulamadı veya bağlantı koptu.';
        }
        this.cdr.detectChanges();
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/alici-hesap-giris']);
  }
}