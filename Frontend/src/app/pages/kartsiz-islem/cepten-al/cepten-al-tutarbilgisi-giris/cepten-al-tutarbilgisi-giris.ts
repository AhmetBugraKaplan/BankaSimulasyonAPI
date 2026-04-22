import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cepten-al-tutarbilgisi-giris',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cepten-al-tutarbilgisi-giris.html',
  styleUrl: './cepten-al-tutarbilgisi-giris.css',
})
export class CeptenAlTutarbilgisiGiris implements AfterViewInit {

  @ViewChild('tutarInput') tutarInput!: ElementRef;
  tutar: number | null = null;
  hataMesaji: string = '';

  constructor(private router: Router) {}

  ngAfterViewInit(): void {
    setTimeout(() => this.tutarInput.nativeElement.focus(), 100);
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
    sessionStorage.setItem('ceptenAlTutar', this.tutar.toString());
    this.router.navigate(['/cepten-al-smsonay-giris']);
  }

  geriDon(): void {
    this.router.navigate(['/cepten-al-gonderen-cepno-giris']);
  }
}