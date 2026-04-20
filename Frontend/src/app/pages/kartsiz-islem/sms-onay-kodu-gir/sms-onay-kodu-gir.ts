import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sms-onay-kodu-gir',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sms-onay-kodu-gir.html',
  styleUrl: './sms-onay-kodu-gir.css',
})
export class SmsOnayKoduGir {

  kod: string = '';
  hataMesaji: string = '';

  constructor(private router: Router) {}

  kodGirildi(): void {
    this.kod = this.kod.replace(/[^0-9]/g, '').slice(0, 4);
    this.hataMesaji = '';
  }

  onayla(): void {
    if (this.kod.length < 4) {
      this.hataMesaji = 'Lütfen 4 haneli kodu eksiksiz giriniz.';
      return;
    }
    // Backend entegrasyonu buraya gelecek
    // Doğruysa:
    this.router.navigate(['/kartsiz-islem-menu']);
  }

  geriDon(): void {
    this.router.navigate(['/tel-no-gir']);
  }
}