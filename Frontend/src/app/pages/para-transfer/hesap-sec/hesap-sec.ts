import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

export interface Hesap {
  hesapNumara: string;
  hesapTip: string;
  hesapBakiye: number;
  paraBirimi: string;
}

@Component({
  selector: 'app-hesap-sec',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hesap-sec.html',
  styleUrl: './hesap-sec.css',
})
export class HesapSec {

  secilenHesap: Hesap | null = null;

  // Backend'den gelene kadar varsayımsal data
  hesaplar: Hesap[] = [
    {
      hesapNumara: '0015 8009 9160 0920 3',
      hesapTip: 'Vadesiz TL',
      hesapBakiye: 12450.00,
      paraBirimi: 'TL'
    },
    {
      hesapNumara: '0015 8009 9160 0921 7',
      hesapTip: 'Vadeli TL',
      hesapBakiye: 85200.00,
      paraBirimi: 'TL'
    },
    {
      hesapNumara: '0015 8009 9160 0922 1',
      hesapTip: 'Döviz USD',
      hesapBakiye: 3800.00,
      paraBirimi: 'USD'
    }
  ];

  constructor(private router: Router) {}

  hesapSec(hesap: Hesap): void {
    this.secilenHesap = hesap;
    // Seçilen hesabı sessionStorage'a kaydet, sonraki adımda kullanılacak
    sessionStorage.setItem('secilenHesap', JSON.stringify(hesap));
    this.router.navigate(['/alici-hesap-giris']);
  }

  geriDon(): void {
    this.router.navigate(['/havale-tipi']);
  }

  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}