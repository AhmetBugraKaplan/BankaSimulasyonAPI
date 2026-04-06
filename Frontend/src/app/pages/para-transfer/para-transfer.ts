import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-para-transfer',
  imports: [CommonModule, FormsModule],
  templateUrl: './para-transfer.html',
  styleUrl: './para-transfer.css',
})
export class ParaTransfer {

  // Mevcut adım: 1=İşlem Tipi, 2=Hesap Seçimi, 3=Tutar Girişi
  adim: number = 1;

  // Seçilen işlem tipi
  secilenIslem: string = '';

  constructor(private router: Router) {}

  islemSecimi(islem: string): void {
    if (islem == 'Havale'){
      this.router.navigate(['/havale-tipi'])
    }
  }

  // Geri butonu — bir önceki adıma veya ATM ana menüsüne dön
  geriDon(): void {
    if (this.adim > 1) {
      this.adim--;
    } else {
      this.router.navigate(['/atm']);
    }
  }

  // Ana Menü butonu
  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}