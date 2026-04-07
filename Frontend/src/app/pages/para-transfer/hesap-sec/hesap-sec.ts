import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Hesap, HesapService } from '../../../services/hesap';

@Component({
  selector: 'app-hesap-sec',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hesap-sec.html',
  styleUrl: './hesap-sec.css',
})


export class HesapSec implements OnInit {

  secilenHesap: Hesap | null = null;
  hesaplar: Hesap[] = [];
  yukleniyor = true;
  hata: string | null = null;

  constructor(
    private router: Router,
    private hesapService: HesapService,
    private cdr: ChangeDetectorRef 
  ) { }

  ngOnInit(): void {
    console.log("1. ngOnInit başladı.");
    const kartNumara = sessionStorage.getItem('kartNumara');
    console.log("2. Session'dan okunan kartNumara:", kartNumara);

    if (!kartNumara) {
      this.hata = 'Oturum bulunamadı.';
      this.yukleniyor = false;
      this.router.navigate(['/login']);
      return;
    }

    console.log("3. Backend'e istek atılıyor...");
    this.hesapService.musteriTumHesaplariGetir(kartNumara).subscribe({
      next: (response) => {
        console.log("4. Backend'den CEVAP GELDİ:", response); // Cevap buraya düşüyor mu?
        
        if (response.islemBasariliMi) {
            console.log("5. İşlem başarılı, hesaplar listeye atanıyor...");
            this.hesaplar = response.data; 
        } else {
            console.log("5. İşlem başarısız, hata mesajı:", response.mesaj);
            this.hata = response.mesaj;
        }
        
        console.log("6. Yükleniyor durumu kapatılıyor (false yapılıyor).");
        this.yukleniyor = false;

        this.cdr.detectChanges();
      },
      error: (err: any) => {
        console.error('4. API HATASI FIRLATILDI:', err);
        this.hata = 'Sunucu ile iletişim kurulamadı.';
        this.yukleniyor = false;
        this.cdr.detectChanges(); 
      }
    });
  }

  hesapSec(hesap: Hesap): void {
    this.secilenHesap = hesap;
    // Senior Notu: Obje verisini string'e çevirip session'a atmak mantıklı bir ara adımdır.
    sessionStorage.setItem('secilenHesap', JSON.stringify(hesap));

    // Alıcı hesap bilgilerini gireceği ekrana yönlendir
    this.router.navigate(['/alici-hesap-giris']);
  }

  geriDon(): void {
    this.router.navigate(['/havale-tipi']);
  }

  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}