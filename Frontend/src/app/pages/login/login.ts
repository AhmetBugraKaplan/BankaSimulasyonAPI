import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Kart } from '../../services/kart';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  kartNo: string = '';
  kartSifre: string = '';
  atmId: string = '';
  hataMesaji: string = '';
  yanlisDeneme: number = 0;

  constructor(private kartService: Kart, private router: Router) { }

  girisYap() {
    this.kartService.kartDogrula(this.kartNo, this.kartSifre, Number(this.atmId)).subscribe(sonuc => {
      if (sonuc.islemBasariliMi) {
        sessionStorage.setItem('token', sonuc.data.token)
        sessionStorage.setItem('kartNumara', sonuc.data.kartNumara);
        sessionStorage.setItem('atmId', sonuc.data.atmId.toString())
        this.router.navigate(['/atm']);
      } else if (sonuc.mesaj === 'Yanlış Şifre') {
        this.yanlisDeneme = sonuc.data
        if (this.yanlisDeneme >= 3) {
          this.router.navigate(['/para-transfer']);
        } else {
          this.hataMesaji = `Yanlış şifre! ${3 - this.yanlisDeneme} deneme hakkınız kaldı.`;
        }
      } else {
        this.hataMesaji = sonuc.mesaj;
      }
    });
  }

  kartsizIslemler(): void {
    this.router.navigate(['/tel-no-gir'])
  }

  qrIslemler(): void {
    // Görsellik için şimdilik boş
  }
}