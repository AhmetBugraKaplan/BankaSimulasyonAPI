import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HesapService } from '../../../../services/hesap';

@Component({
  selector: 'app-gonderilecek-tutar-gir',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './gonderilecek-tutar-gir.html',
  styleUrl: './gonderilecek-tutar-gir.css',
})
export class GonderilecekTutarGir implements OnInit, AfterViewInit {

  @ViewChild('tutarInput') tutarInput!: ElementRef;

  aliciHesapNo: string = '';
  tutar: number | null = null;
  hataMesaji: string = '';

  constructor(private router: Router,
    private hesapService: HesapService) { }

  ngOnInit(): void {
    const hesapNo = sessionStorage.getItem('kartsizAliciHesapNo');
    if (!hesapNo) {
      this.router.navigate(['/alici-hesap-no-gir']);
      return;
    }
    this.aliciHesapNo = hesapNo;
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.tutarInput.nativeElement.focus();
    }, 100);
  }

  tutarGirildi(): void {
    this.hataMesaji = '';
    if (this.tutar && this.tutar > 5000) {
      this.tutar = 5000;
      this.hataMesaji = 'Maksimum tutar 5.000 ₺ olabilir.';
    }
  }

  devam(): void {
    if (!this.tutar || this.tutar <= 0) {
      this.hataMesaji = 'Lütfen geçerli bir tutar giriniz.';
      return;
    }
    if (this.tutar > 5000) {
      this.hataMesaji = 'Maksimum tutar 5.000 ₺ olabilir.';
      return;
    }

    this.hesapService.hesabaKartsizParaGonder(this.aliciHesapNo, this.tutar).subscribe({
      next: (response) => {
        if (response.islemBasariliMi) {
          sessionStorage.setItem('gonderilecekTutar', this.tutar!.toString());
          this.router.navigate(['/kartsiz-islem-onaylandi']);
        } else {
          this.hataMesaji = response.mesaj || 'Transfer gerçekleştirilemedi.';
        }
      },
      error: (err) => {
        console.error('Hata', err);
        this.hataMesaji = 'Bir hata oluştu, tekrar deneyiniz.';
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/alici-hesap-no-gir']);
  }
}