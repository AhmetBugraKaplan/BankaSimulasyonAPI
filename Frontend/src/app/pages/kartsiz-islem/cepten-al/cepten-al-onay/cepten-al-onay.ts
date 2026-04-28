import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HesapService } from '../../../../services/hesap';

@Component({
  selector: 'app-cepten-al-onay',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cepten-al-onay.html',
  styleUrl: './cepten-al-onay.css',
})
export class CeptenAlOnay implements OnInit {

  tcNo: string = '';
  kendiTelNo: string = '';
  gonderenTelNo: string = '';
  tutar: string = '';
  hataMesaji: string = '';
  yukleniyor: boolean = false;

  constructor(
    private router: Router,
    private hesapService: HesapService,
    private cdr: ChangeDetectorRef       // ⭐ EKLENDİ
  ) {}

  ngOnInit(): void {
    const tc = sessionStorage.getItem('ceptenAlTcNo') ?? '';
    const kendiTel = sessionStorage.getItem('ceptenAlKendiTelNo') ?? '';
    const gonderenTel = sessionStorage.getItem('ceptenAlGonderenTelNo') ?? '';
    const tutar = sessionStorage.getItem('ceptenAlTutar') ?? '';

    if (!tc || !kendiTel || !gonderenTel || !tutar) {
      this.router.navigate(['/cepten-al-tc-giris']);
      return;
    }

    this.tcNo = tc;
    this.kendiTelNo = kendiTel.slice(0,4) + '-' + kendiTel.slice(4,7) + '-' + kendiTel.slice(7,9) + '-' + kendiTel.slice(9,11);
    this.gonderenTelNo = gonderenTel.slice(0,4) + '-' + gonderenTel.slice(4,7) + '-' + gonderenTel.slice(7,9) + '-' + gonderenTel.slice(9,11);
    this.tutar = tutar;
  }

  onayla(): void {
    if (this.yukleniyor) return;

    this.hataMesaji = '';
    this.yukleniyor = true;
    this.cdr.detectChanges();  

    const tcNo = sessionStorage.getItem('ceptenAlTcNo') ?? '';
    const kendiTel = sessionStorage.getItem('ceptenAlKendiTelNo') ?? '';
    const gonderenTel = sessionStorage.getItem('ceptenAlGonderenTelNo') ?? '';
    const tutar = parseFloat(sessionStorage.getItem('ceptenAlTutar') ?? '0');

    this.hesapService.cebeParaCek(tcNo, kendiTel, gonderenTel, tutar).subscribe({
      next: (sonuc) => {
        this.yukleniyor = false;

        if (sonuc.islemBasariliMi) {
          this.router.navigate(['/cepten-al-paraayal']);
        } else {
          this.hataMesaji = sonuc.mesaj;
        }

        this.cdr.detectChanges(); 
      },
      error: (err) => {
        console.error('Hata:', err);
        this.yukleniyor = false;
        this.hataMesaji = 'İşlem sırasında bir hata oluştu. Lütfen tekrar deneyin.';
        this.cdr.detectChanges();   
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/cepten-al-tutarbilgisi-giris']);
  }
}