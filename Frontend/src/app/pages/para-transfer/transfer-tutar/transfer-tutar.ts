import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Hesap } from '../../../models/hesap.model';

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
  hataMesaji: string = '';

  constructor(private router: Router) {}

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
    if (!this.tutar || this.tutar <= 0) {
      this.hataMesaji = 'Lütfen geçerli bir tutar giriniz.';
      return;
    }

    if (this.secilenHesap && this.tutar > this.secilenHesap.hesapBakiye) {
      this.hataMesaji = 'Yetersiz bakiye.';
      return;
    }

    sessionStorage.setItem('transferTutar', this.tutar.toString());
    // Sonraki adım: onay ekranı veya direkt transfer işlemi
    this.router.navigate(['/transfer-onay']);
  }

  geriDon(): void {
    this.router.navigate(['/alici-hesap-giris']);
  }

  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}