import { Component, AfterViewInit, OnDestroy, ViewChild, ElementRef, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Onay } from '../../../../services/onay';

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

  readonly cember = 2 * Math.PI * 26;
  dashOffset: number = 0;

  private interval: any;

  constructor(
    private router: Router,
    private ngZone: NgZone,
    private onayService: Onay
  ) {}

  ngAfterViewInit(): void {
    this.focusInput();
    this.sayaciBaslat();
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

    const telNo = sessionStorage.getItem('ceptenAlKendiTelNo') ?? '';

    this.onayService.onayKodUret(telNo).subscribe({
      next: (response) => {
        console.log('Kod tekrar üretildi:', response);
        this.sayaciBaslat();
        this.focusInput();
      },
      error: (err) => {
        console.error('Hata:', err);
        this.hataMesaji = 'SMS tekrar gönderilirken bir hata oluştu.';
      }
    });
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

    const telNo = sessionStorage.getItem('ceptenAlKendiTelNo') ?? '';

    this.onayService.onayKoduDogruMu(this.kod, telNo).subscribe({
      next: (sonuc) => {
        if (sonuc.islemBasariliMi) {
          this.router.navigate(['/cepten-al-gonderen-cepno-giris']);
        } else {
          this.hataMesaji = 'Girilen kod hatalı veya süresi dolmuş.';
          this.kod = '';
        }
      },
      error: (err) => {
        console.error('Hata:', err);
        this.hataMesaji = 'Doğrulama sırasında bir hata oluştu.';
      }
    });
  }

  geriDon(): void {
    this.kod = '';
    this.hataMesaji = '';
    clearInterval(this.interval);
    this.router.navigate(['/cepten-al-cepno-giris']);
  }
}