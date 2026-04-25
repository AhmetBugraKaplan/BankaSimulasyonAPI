import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-kartsiz-islem-menu',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './kartsiz-islem-menu.html',
  styleUrl: './kartsiz-islem-menu.css',
})
export class KartsizIslemMenu {

  constructor(private router: Router) { }

  baskasininHesabina(): void {
    this.router.navigate(['/alici-hesap-no-gir']);
  }
  
  ceptenParaAl(): void {
    this.router.navigate(['/cepten-al-tc-giris']);
  }

  geriDon(): void {
    this.router.navigate(['/sms-onay-kodu-gir']);
  }

  anaMenuye(): void {
    this.router.navigate(['/']);
  }
}