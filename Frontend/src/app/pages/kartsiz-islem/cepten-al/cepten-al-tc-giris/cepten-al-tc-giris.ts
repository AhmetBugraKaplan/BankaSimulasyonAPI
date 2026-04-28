import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cepten-al-tc-giris',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cepten-al-tc-giris.html',
  styleUrl: './cepten-al-tc-giris.css',
})
export class CeptenAlTcGiris implements AfterViewInit {

  @ViewChild('tcInput') tcInput!: ElementRef;
  tcNo: string = '';
  hataMesaji: string = '';

  constructor(private router: Router) {}

  ngAfterViewInit(): void {
    setTimeout(() => this.tcInput.nativeElement.focus(), 100);
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
  

    sessionStorage.setItem('ceptenAlTcNo', this.tcNo);
    this.router.navigate(['/cepten-al-cepno-giris']);
  }

  anaMenuye(): void {
    sessionStorage.clear();
    this.router.navigate(['/']);
  }
}