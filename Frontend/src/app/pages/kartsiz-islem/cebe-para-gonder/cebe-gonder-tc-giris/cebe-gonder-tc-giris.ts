import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cebe-gonder-tc-giris',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cebe-gonder-tc-giris.html',
  styleUrl: './cebe-gonder-tc-giris.css',
})
export class CebeGonderTcGiris implements AfterViewInit {

  @ViewChild('tcInput') tcInput!: ElementRef;

  tcNo: string = '';
  hataMesaji: string = '';

  constructor(private router: Router) {}

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.tcInput.nativeElement.focus();
    }, 100);
  }

  tcGirildi(): void {
    this.tcNo = this.tcNo.replace(/[^0-9]/g, '').slice(0, 11);
    this.hataMesaji = '';
  }

  devam(): void {
    if (this.tcNo.length < 11) {
      this.hataMesaji = 'Lütfen geçerli bir T.C. Kimlik No giriniz.';
      return;
    }

    // TC doğrulama servisi buraya gelecek
    // this.tcService.tcDogrula(this.tcNo).subscribe(sonuc => {
    //   if (sonuc.islemBasariliMi) {
    //     sessionStorage.setItem('cebeGonderTcNo', this.tcNo);
    //     this.router.navigate(['/cebe-gonder-kendi-telno-giris']);
    //   } else {
    //     this.hataMesaji = sonuc.mesaj;
    //   }
    // });

    // Servis entegrasyonu tamamlanana kadar direkt geçiş:
    sessionStorage.setItem('cebeGonderTcNo', this.tcNo);
    this.router.navigate(['/cebe-gonder-kendi-telno-giris']);
  }

  anaMenuye(): void {
    sessionStorage.clear();
    this.router.navigate(['/']);
  }
}