import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Onay } from '../../../../services/onay';

@Component({
  selector: 'app-sms-onay-kodu-gir',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sms-onay-kodu-gir.html',
  styleUrl: './sms-onay-kodu-gir.css',
})
export class SmsOnayKoduGir implements AfterViewInit {

  @ViewChild('kodInput') kodInput!: ElementRef;

  kod: string = '';
  hataMesaji: string = '';

  constructor(private router: Router, private onayService: Onay) { }

  ngAfterViewInit(): void {
    setTimeout(() => this.kodInput.nativeElement.focus(), 100);
  }

  kodGirildi(): void {
    this.kod = this.kod.replace(/[^0-9]/g, '').slice(0, 4);
    this.hataMesaji = '';
  }

  inputBlur(): void {
    setTimeout(() => this.kodInput.nativeElement.focus(), 0);
  }

  onayla(): void {
    if (this.kod.length < 4) {
      console.log(this.kod.length);
      this.hataMesaji = 'Lütfen 4 haneli kodu eksiksiz giriniz.';
      return;
    }

    const telNo = sessionStorage.getItem('telNo') ?? '';

    this.onayService.onayKoduDogruMu(this.kod, telNo).subscribe({
      next: (response) => {
        if (response.islemBasariliMi) {
          this.router.navigate(['/kartsiz-islem-menu']);
        } else {
          this.hataMesaji = 'Girdiğiniz doğrulama kodu hatalı veya süresi dolmuş';
          this.kod = '';
          setTimeout(() => this.kodInput.nativeElement.focus(), 100);
        }
      },
      error: (err) => {
        console.error('Hata', err);
        this.hataMesaji = 'Bir hata oluştu, tekrar deneyiniz.';
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/tel-no-gir']);
  }
}