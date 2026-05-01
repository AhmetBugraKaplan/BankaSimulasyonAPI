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
      this.yanlisDeneme = sonuc.data;
      //İşlem başarılıysa
      if (sonuc.islemBasariliMi) {
        sessionStorage.setItem('token', sonuc.data.token)
        sessionStorage.setItem('kartNumara', sonuc.data.kartNumara);
        sessionStorage.setItem('atmId', sonuc.data.atmId.toString())
        this.router.navigate(['/atm']);
      }
      //kartDogrula.Data içinde yanlış giriş sayısı var yani yanlış giriş sayısı değişkenimiz kartlar tablosunda tutuluyor ve buradan getiriliyor.
      else if (sonuc.mesaj === 'Yanlış Şifre') {
        this.yanlisDeneme = sonuc.data
        if (this.yanlisDeneme >= 3) {
          this.router.navigate(['/sifre-degistir']);
        } else {
          this.hataMesaji = `Yanlış şifre! ${3 - this.yanlisDeneme} deneme hakkınız kaldı.`;
        }
      }
      else if(sonuc.mesaj == "Kart bloke edilmiştir"){
          this.router.navigate(['/sifre-degistir']);
      }
      else {
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