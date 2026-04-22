import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Hesap, HesapService } from '../../../../services/hesap';

@Component({
  selector: 'app-alici-hesap-no-gir',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './alici-hesap-no-gir.html',
  styleUrl: './alici-hesap-no-gir.css',
})
export class AliciHesapNoGir implements AfterViewInit {

  @ViewChild('hesapInput') hesapInput!: ElementRef;

  aliciHesapNo: string = '';
  hataMesaji: string = '';

  constructor(private router: Router,
    private hesapService: HesapService) { }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.hesapInput.nativeElement.focus();
    }, 100);
  }

  devam(): void {
    if (!this.aliciHesapNo.trim()) {
      this.hataMesaji = 'Lütfen alıcı hesap numarasını giriniz.';
      return;
    }

    this.hesapService.hesapVarMi(this.aliciHesapNo.trim()).subscribe({
      next: (response) => {
        if (response.islemBasariliMi) {
          sessionStorage.setItem('kartsizAliciHesapNo', this.aliciHesapNo.trim());
          this.router.navigate(['/gonderilecek-tutar-gir']);
        } else {
          this.hataMesaji = 'Girilen hesap numarası sistemde bulunamadı.';
        }
      },
      error: (err) => {
        console.error('Hata', err);
        this.hataMesaji = 'Bir hata oluştu, tekrar deneyiniz.';
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/kartsiz-islem-menu']);
  }
}