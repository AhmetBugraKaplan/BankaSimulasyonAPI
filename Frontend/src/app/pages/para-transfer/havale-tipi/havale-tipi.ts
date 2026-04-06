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
    if (tip === 'BaskasininHesabi') {
      this.router.navigate(['/hesap-sec']);
    }
  }

  geriDon(): void {
    this.router.navigate(['/para-transfer']);
  }

  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}