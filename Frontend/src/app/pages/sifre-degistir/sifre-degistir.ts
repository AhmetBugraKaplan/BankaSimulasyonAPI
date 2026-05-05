import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Kart } from '../../services/kart';

@Component({
  selector: 'app-sifre-degistir',
  imports: [FormsModule, CommonModule],
  templateUrl: './sifre-degistir.html',
  styleUrl: './sifre-degistir.css',
})
export class SifreDegistir {
  kartNo: string = '';
  yeniSifre: string = '';
  yeniSifreTekrar: string = '';
  hataMesaji: string = '';
  basariMesaji: string = '';

  constructor(private kartService: Kart, private router: Router) {}

  sifreDegistir() {
    if (this.yeniSifre !== this.yeniSifreTekrar) {
      this.hataMesaji = 'Şifreler eşleşmiyor!';
      return;
    }

    this.kartService.kartSifreGuncelle(this.kartNo, this.yeniSifre).subscribe(sonuc => {
      if (sonuc.islemBasariliMi) {
        this.basariMesaji = 'Şifreniz güncellendi, giriş sayfasına yönlendiriliyorsunuz...';
        setTimeout(() => this.router.navigate(['/']), 1000);
      } else {
        this.hataMesaji = sonuc.mesaj;
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/']);
  }
}