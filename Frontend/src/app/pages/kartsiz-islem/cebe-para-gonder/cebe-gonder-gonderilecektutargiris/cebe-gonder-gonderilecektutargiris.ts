import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cebe-gonder-gonderilecektutargiris',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cebe-gonder-gonderilecektutargiris.html',
  styleUrl: './cebe-gonder-gonderilecektutargiris.css',
})
export class CebeGonderGonderilecektutargiris implements OnInit, AfterViewInit {

  @ViewChild('tutarInput') tutarInput!: ElementRef;

  aliciTelNo: string = '';
  tutar: number | null = null;
  hataMesaji: string = '';

  constructor(private router: Router) {}

  ngOnInit(): void {
    const tel = sessionStorage.getItem('cebeGonderTelNo');
    if (!tel) {
      this.router.navigate(['/cebe-gonder-kendi-telno-giris']);
      return;
    }
    const raw = tel;
    this.aliciTelNo = raw.slice(0,4) + '-' + raw.slice(4,7) + '-' + raw.slice(7,9) + '-' + raw.slice(9,11);
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

    // Cebe para gönder servisi buraya gelecek
    // this.kartsizIslemService.cebeParaGonder(
    //   sessionStorage.getItem('cebeGonderTcNo'),
    //   sessionStorage.getItem('cebeGonderTelNo'),
    //   this.tutar
    // ).subscribe(sonuc => {
    //   if (sonuc.islemBasariliMi) {
    //     this.router.navigate(['/kartsiz-islem-onaylandi']);
    //   } else {
    //     this.hataMesaji = sonuc.mesaj;
    //   }
    // });

    // Servis entegrasyonu tamamlanana kadar direkt geçiş:
    sessionStorage.setItem('cebeGonderTutar', this.tutar.toString());
    this.router.navigate(['/kartsiz-islem-onaylandi']);
  }

  geriDon(): void {
    this.router.navigate(['/cebe-gonder-kendi-telno-giris']);
  }
}