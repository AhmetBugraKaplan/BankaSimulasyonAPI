import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Hesap, HesapService } from '../../../services/hesap';

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

  constructor(private router: Router, private hesapService: HesapService) { }

  ngOnInit(): void {
    const tel = sessionStorage.getItem('cebeGonderAliciTelNo');
    if (!tel) {
      this.router.navigate(['/cebe-gonder-alici-telno-giris']);
      return;
    }
    const raw = tel;
    this.aliciTelNo = raw.slice(0, 4) + '-' + raw.slice(4, 7) + '-' + raw.slice(7, 9) + '-' + raw.slice(9, 11);
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
    const tutar = this.tutar;
    if (!tutar || tutar <= 0) {
      this.hataMesaji = 'Lütfen geçerli bir tutar giriniz.';
      return;
    }

    if (tutar > 5000) {
      this.hataMesaji = 'Maksimum tutar 5.000 ₺ olabilir.';
      return;
    }

    const gonderenKartNo = sessionStorage.getItem('kartNumara');
    const aliciTckNO = sessionStorage.getItem('cebeGonderAliciTcNo');
    const aliciTelNo = sessionStorage.getItem('cebeGonderAliciTelNo');

    if (!gonderenKartNo) {
      this.hataMesaji = "Oturum bilgisi eksik, lütfen tekrar giriş yapın.";
      return;
    }

    if (!aliciTckNO || !aliciTelNo) {
      this.hataMesaji = "Alıcı kimlik numarası ya da telefon numarası eksik!";
      return;
    }

    this.hesapService.cebeParaGonder(
      gonderenKartNo,
      aliciTckNO,
      aliciTelNo,
      tutar
    ).subscribe(sonuc => {
      if (sonuc.islemBasariliMi) {
        this.router.navigate(['/kartsiz-islem-onaylandi']);
      } else {
        this.hataMesaji = sonuc.mesaj;
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/cebe-gonder-kendi-telno-giris']);
  }
}