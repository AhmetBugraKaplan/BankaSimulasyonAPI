import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HesapService } from '../../../../services/hesap';
import { Onay } from '../../../../services/onay';

@Component({
  selector: 'app-tel-no-gir',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tel-no-gir.html',
  styleUrl: './tel-no-gir.css',
})

export class TelNoGir implements OnInit {

  HesapVarMi: boolean = false;
  telNo: string = '05';
  hataMesaji: string = '';

  constructor(private router: Router,
    private hesapService: HesapService,
    private onayService: Onay) {
  }

  //ngOnInit ne işe yarar ? = Constructordan sonra çalışan fonksiyondur.
  //Atama işlemlerini burda yaparız constructor içinde DI işlemleri yapılır. 
  
  ngOnInit(): void {
    this.telNo = '05';
  }

  telNoGirildi(event: Event): void {
    const input = event.target as HTMLInputElement;
    let raw = input.value.replace(/[^0-9]/g, '');

    // 05 koruması
    if (!raw.startsWith('05')) {
      raw = '05' + raw.replace(/^0*5*/, '');
    }

    raw = raw.slice(0, 11);

    // Format: 0552-216-12-98
    let formatted = raw;
    if (raw.length > 4 && raw.length <= 7) {
      formatted = raw.slice(0, 4) + '-' + raw.slice(4);
    } else if (raw.length > 7 && raw.length <= 9) {
      formatted = raw.slice(0, 4) + '-' + raw.slice(4, 7) + '-' + raw.slice(7);
    } else if (raw.length > 9) {
      formatted = raw.slice(0, 4) + '-' + raw.slice(4, 7) + '-' + raw.slice(7, 9) + '-' + raw.slice(9, 11);
    }

    this.telNo = formatted;
    this.hataMesaji = '';
  }

  devam(): void {
    const raw = this.telNo.replace(/[^0-9]/g, '');
    if (raw.length < 11) {
      this.hataMesaji = 'Lütfen geçerli bir telefon numarası giriniz.';
      return;
    }
    sessionStorage.setItem('telNo', raw);

    //TelefonNoVar ise burda geçmiş işlemler tablosuna hesapno ile kaydedilecek.

    let hesapVarMi: boolean;

    this.hesapService.hesapVarMiTelNoIle(raw).subscribe({
      next: (response) => {
        this.HesapVarMi = response.islemBasariliMi; // BaseResponse içindeki boolean burda
        if (this.HesapVarMi) {
          //Burda işlemi müşterinin geçmiş işlemlerine kaydedicez ama bu yapıyı sonrasında kuracağım
          console.log("HesapVar çalışıyor")
        }
        console.log('Hesap var mı:', this.HesapVarMi);
      },
      error: (err) => {
        console.error('Hata:', err);
      }
    })

    this.onayService.onayKodUret(raw).subscribe({
      next: (response) => console.log('Kod üretildi:', response),
      error: (err) => console.error('Hata:', err)
    }); this.router.navigate(['/sms-onay-kodu-gir']);
  }

  geriDon(): void {
    this.router.navigate(['/']);
  }
}