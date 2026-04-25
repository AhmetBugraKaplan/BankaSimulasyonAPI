import { Component, AfterViewInit, OnDestroy, ViewChild, ElementRef, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cepten-al-sms-dogrulama',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cepten-al-sms-dogrulama.html',
  styleUrl: './cepten-al-sms-dogrulama.css',
})
export class CeptenAlSmsDogrulama implements AfterViewInit, OnDestroy {

  @ViewChild('kodInput') kodInput!: ElementRef;

  kod: string = '';
  hataMesaji: string = '';
  kalanSure: number = 60;
  tekrarGonderAktif: boolean = false;

  readonly cember = 2 * Math.PI * 26; // ≈ 163.36
  dashOffset: number = 0;

  private interval: any;

  constructor(private router: Router, private ngZone: NgZone) {}

  ngAfterViewInit(): void {
    this.focusInput();
    this.sayaciBaslat();

    // Her tıklamada focus'u geri al
    document.addEventListener('click', this.focusInput.bind(this));
    document.addEventListener('keydown', this.focusInput.bind(this));
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
    document.removeEventListener('click', this.focusInput.bind(this));
    document.removeEventListener('keydown', this.focusInput.bind(this));
  }

  focusInput(): void {
    setTimeout(() => {
      this.kodInput?.nativeElement?.focus();
    }, 50);
  }

  sayaciBaslat(): void {
    this.kalanSure = 60;
    this.tekrarGonderAktif = false;
    this.dashOffset = 0;
    clearInterval(this.interval);

    // NgZone içinde çalıştır ki Angular değişiklik tespiti çalışsın
    this.ngZone.runOutsideAngular(() => {
      this.interval = setInterval(() => {
        this.ngZone.run(() => {
          this.kalanSure--;
          this.dashOffset = this.cember * (1 - this.kalanSure / 60);

          if (this.kalanSure <= 0) {
            clearInterval(this.interval);
            this.tekrarGonderAktif = true;
          }
        });
      }, 1000);
    });
  }

  tekrarGonder(): void {
    if (!this.tekrarGonderAktif) return;
    this.kod = '';
    this.hataMesaji = '';

    // SMS tekrar gönder servisi buraya gelecek
    // this.smsService.smsTekrarGonder(sessionStorage.getItem('ceptenAlKendiTelNo')).subscribe();

    this.sayaciBaslat();
    this.focusInput();
  }

  kodGirildi(): void {
    this.kod = this.kod.replace(/[^0-9]/g, '').slice(0, 4);
    this.hataMesaji = '';
  }

  giris(): void {
    if (this.kod.length < 4) {
      this.hataMesaji = 'Lütfen 4 haneli kodu eksiksiz giriniz.';
      return;
    }
    // SMS doğrulama servisi buraya gelecek
    // this.smsService.smsDogrula(this.kod).subscribe(sonuc => {
    //   if (sonuc.islemBasariliMi) {
    //     this.router.navigate(['/cepten-al-gonderen-cepno-giris']);
    //   } else {
    //     this.hataMesaji = sonuc.mesaj;
    //   }
    // });

    this.router.navigate(['/cepten-al-gonderen-cepno-giris']);
  }

  geriDon(): void {
    this.kod = '';
    this.hataMesaji = '';
    clearInterval(this.interval);
    this.router.navigate(['/cepten-al-cepno-giris']);
  }
}