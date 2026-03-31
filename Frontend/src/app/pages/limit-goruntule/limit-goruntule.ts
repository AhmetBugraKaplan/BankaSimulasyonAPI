import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Kart } from '../../services/kart';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-limit-goruntule',
  imports: [CommonModule],
  templateUrl: './limit-goruntule.html',
  styleUrl: './limit-goruntule.css'
})

export class LimitGoruntule implements OnInit {
  kalanLimit: number = 0;
  musteriLimit: number = 0;
  hataMesaji: string = '';

constructor(private kartService: Kart, private router: Router, private cdr: ChangeDetectorRef) { }

  //ngOnInit sayfa açılınca otomatik tetikliyor.
  ngOnInit() {
  const token = localStorage.getItem('token');
  if (!token) {
    this.router.navigate(['/']);
    return;
  }

  this.kartService.kartKalanLimitGetir().subscribe(sonuc => {
    console.log('limit API', sonuc);

    // Büyük-küçük harf kontrolü
    const basarili = sonuc.islemBasariliMi ?? (sonuc as any).IslemBasariliMi;

    if (basarili) {
      this.kalanLimit = sonuc.data;   // veya (sonuc as any).Data
      this.hataMesaji = '';
    } else {
      this.hataMesaji = sonuc.mesaj || 'Limit bilgisi alınamadı.';
    }

    this.cdr.detectChanges();

  }, err => {
    this.hataMesaji = 'Sunucu bağlantı hatası';
    this.cdr.detectChanges();
  });
}

  geriDon() {
    this.router.navigate(['/atm']);
  }
}
