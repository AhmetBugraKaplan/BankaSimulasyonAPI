import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

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

  constructor(private router: Router) {}

  ngOnInit(): void {
    const tc = sessionStorage.getItem('ceptenAlTcNo') ?? '';
    const kendiTel = sessionStorage.getItem('ceptenAlKendiTelNo') ?? '';
    const gonderenTel = sessionStorage.getItem('ceptenAlGonderenTelNo') ?? '';
    const tutar = sessionStorage.getItem('ceptenAlTutar') ?? '';

    this.tcNo = tc;
    this.kendiTelNo = kendiTel.slice(0,4)+'-'+kendiTel.slice(4,7)+'-'+kendiTel.slice(7,9)+'-'+kendiTel.slice(9,11);
    this.gonderenTelNo = gonderenTel.slice(0,4)+'-'+gonderenTel.slice(4,7)+'-'+gonderenTel.slice(7,9)+'-'+gonderenTel.slice(9,11);
    this.tutar = tutar;
  }

  onayla(): void {
    // Onay servisi buraya gelecek
    // this.ceptenAlService.onayla({...}).subscribe(sonuc => {
    //   if (sonuc.islemBasariliMi) {
    //     this.router.navigate(['/cepten-al-paraayal']);
    //   } else {
    //     // hata mesajı
    //   }
    // });

    this.router.navigate(['/cepten-al-paraayal']);
  }

  geriDon(): void {
    this.router.navigate(['/cepten-al-smsonay-giris']);
  }
}