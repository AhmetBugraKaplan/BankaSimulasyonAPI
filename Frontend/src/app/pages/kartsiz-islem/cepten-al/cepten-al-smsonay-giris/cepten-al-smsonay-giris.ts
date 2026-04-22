import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-cepten-al-smsonay-giris',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cepten-al-smsonay-giris.html',
  styleUrl: './cepten-al-smsonay-giris.css',
})
export class CeptenAlSmsonayGiris implements AfterViewInit {

  @ViewChild('kodInput') kodInput!: ElementRef;

  kod: string = '';
  hataMesaji: string = '';

  constructor(private router: Router) {
    // Her bu sayfaya gelinişte focus yenile
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.focusInput();
    });
  }

  ngAfterViewInit(): void {
    this.focusInput();
  }

  focusInput(): void {
    setTimeout(() => {
      this.kodInput?.nativeElement?.focus();
    }, 100);
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
    // SMS onay servisi buraya gelecek
    // this.smsService.smsDogrula(this.kod).subscribe(sonuc => {
    //   if (sonuc.islemBasariliMi) {
    //     this.router.navigate(['/cepten-al-onay']);
    //   } else {
    //     this.hataMesaji = sonuc.mesaj;
    //   }
    // });

    this.router.navigate(['/cepten-al-onay']);
  }

  geriDon(): void {
    this.kod = '';
    this.hataMesaji = '';
    this.router.navigate(['/cepten-al-tutarbilgisi-giris']);
  }
}