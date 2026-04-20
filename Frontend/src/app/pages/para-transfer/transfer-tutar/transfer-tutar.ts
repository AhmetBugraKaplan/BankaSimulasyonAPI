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
  havaletipi: string = '';

  yukleniyor: boolean = false;
  mesaj: string = '';
  islemBasariliMi: boolean = false;

  constructor(
    private router: Router,
    private hesapService: HesapService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.havaletipi = sessionStorage.getItem('havaletipi') || '';

    if (this.havaletipi === 'HesaplarimArasi') {
      const gonderenJson = sessionStorage.getItem('gonderenHesap');
      const aliciJson = sessionStorage.getItem('aliciHesap');

      if (!gonderenJson || !aliciJson) {
        this.router.navigate(['/havale-hesaplararasi-gonderecekhesabisec']);
        return;
      }

      this.secilenHesap = JSON.parse(gonderenJson);
      const aliciHesap: Hesap = JSON.parse(aliciJson);
      this.aliciHesapNumara = aliciHesap.hesapNumara;

    } else if (this.havaletipi === 'BaskasininHesabi') {
      const hesapData = sessionStorage.getItem('secilenHesap');
      const aliciData = sessionStorage.getItem('aliciHesapNumara');

      if (!hesapData || !aliciData) {
        this.router.navigate(['/hesap-sec']);
        return;
      }

      this.secilenHesap = JSON.parse(hesapData);
      this.aliciHesapNumara = aliciData;
    }
  }

  devam(): void {
    this.mesaj = '';

    if (!this.tutar || this.tutar <= 0) {
      this.mesaj = 'Lütfen sıfırdan büyük geçerli bir tutar giriniz.';
      return;
    }

    if (this.secilenHesap && this.tutar > this.secilenHesap.hesapBakiye) {
      this.mesaj = 'Yetersiz bakiye. Girdiğiniz tutar hesabınızdaki parayı aşıyor.';
      return;
    }

    this.yukleniyor = true;
    this.cdr.detectChanges();

    const kartNo = sessionStorage.getItem('kartNumara') || '';
    const talep: HavaleTalebi = {
      GonderenHesapNumara: this.secilenHesap!.hesapNumara,
      AliciHesapNumara: this.aliciHesapNumara,
      GonderilenTutar: this.tutar,
      KartNumara: kartNo
    };

    forkJoin({
      apiCevabi: this.hesapService.havaleYap(talep),
      beklemeSuresi: timer(1000)
    }).subscribe({
      next: (sonuc) => {
        const res = sonuc.apiCevabi;
        this.yukleniyor = false;
        this.cdr.detectChanges();

        if (res.islemBasariliMi) {
          sessionStorage.removeItem('secilenHesap');
          sessionStorage.removeItem('aliciHesapNumara');
          sessionStorage.removeItem('gonderenHesap');
          sessionStorage.removeItem('aliciHesap');
          sessionStorage.removeItem('havaletipi');
          this.router.navigate(['/islem-onaylandi']);
        } else {
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
    if (this.havaletipi === 'HesaplarimArasi') {
      this.router.navigate(['/havale-hesaplararasi-gonderilecekhesabisec']);
    } else {
      this.router.navigate(['/alici-hesap-giris']);
    }
  }
}