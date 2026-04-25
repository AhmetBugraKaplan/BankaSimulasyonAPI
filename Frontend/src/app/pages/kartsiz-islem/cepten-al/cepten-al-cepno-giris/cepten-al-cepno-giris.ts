import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cepten-al-cepno-giris',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cepten-al-cepno-giris.html',
  styleUrl: './cepten-al-cepno-giris.css',
})
export class CeptenAlCepnoGiris implements AfterViewInit {

  @ViewChild('telInput') telInput!: ElementRef;
  telNo: string = '05';
  hataMesaji: string = '';

  constructor(private router: Router) {}

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.telInput.nativeElement.focus();
      this.telInput.nativeElement.setSelectionRange(2, 2);
    }, 100);
  }

  telNoGirildi(event: Event): void {
    const input = event.target as HTMLInputElement;
    let raw = input.value.replace(/[^0-9]/g, '');
    if (!raw.startsWith('05')) raw = '05' + raw.slice(2);
    raw = raw.slice(0, 11);
    let formatted = raw;
    if (raw.length > 4 && raw.length <= 7) formatted = raw.slice(0,4) + '-' + raw.slice(4);
    else if (raw.length > 7 && raw.length <= 9) formatted = raw.slice(0,4) + '-' + raw.slice(4,7) + '-' + raw.slice(7);
    else if (raw.length > 9) formatted = raw.slice(0,4) + '-' + raw.slice(4,7) + '-' + raw.slice(7,9) + '-' + raw.slice(9,11);
    this.telNo = formatted;
    this.hataMesaji = '';
  }

  devam(): void {
    const raw = this.telNo.replace(/[^0-9]/g, '');
    if (raw.length < 11) {
      this.hataMesaji = 'Lütfen geçerli bir telefon numarası giriniz.';
      return;
    }
    sessionStorage.setItem('ceptenAlKendiTelNo', raw);
    this.router.navigate(['/cepten-al-sms-dogrulama']);
  }

  geriDon(): void {
    this.router.navigate(['/cepten-al-tc-giris']);
  }
}