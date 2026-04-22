import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-kartsiz-islem-onaylandi',
  standalone: true,
  imports: [],
  templateUrl: './kartsiz-islem-onaylandi.html',
  styleUrl: './kartsiz-islem-onaylandi.css',
})
export class KartsizIslemOnaylandi {

  constructor(private router: Router) {}

  anaMenuye(): void {
    sessionStorage.clear();
    this.router.navigate(['/']);
  }
}