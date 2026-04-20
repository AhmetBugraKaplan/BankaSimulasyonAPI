import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-havale-tipi',
  standalone: true,
  imports: [],
  templateUrl: './havale-tipi.html',
  styleUrl: './havale-tipi.css',
})
export class HavaleTipi {

  constructor(private router: Router) { }

  havaleSecimi(tip: string): void {
    sessionStorage.setItem('havaletipi',tip);
    if (tip === 'BaskasininHesabi') {
      this.router.navigate(['/hesap-sec']);
    }else if (tip == 'HesaplarimArasi') {
    this.router.navigate(['/havale-hesaplararasi-gonderecekhesabisec']);
}
  }

  geriDon(): void {
    this.router.navigate(['/para-transfer']);
  }

  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}