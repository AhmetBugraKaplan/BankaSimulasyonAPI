import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-islem-onaylandi',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './islem-onaylandi.html',
  styleUrl: './islem-onaylandi.css',
})
export class IslemOnaylandi implements OnInit {

  constructor(private router: Router) {}

  ngOnInit(): void {
    sessionStorage.removeItem('secilenHesap');
    sessionStorage.removeItem('aliciHesapNumara');
    sessionStorage.removeItem('transferTutar');
  }

  anaMenuye(): void {
    // Başka bir işlem yapmak isterse Ana Menüye yönlendir
    this.router.navigate(['/atm']);
  }

  cikisYap(): void {
    // Kart İade / Çıkış seçeneği: Tüm session'ı sil ve login'e at
    sessionStorage.clear();
    this.router.navigate(['/']);
  }
}