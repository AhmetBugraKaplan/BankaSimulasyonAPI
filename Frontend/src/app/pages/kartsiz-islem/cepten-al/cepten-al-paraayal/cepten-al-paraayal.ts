import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cepten-al-paraayal',
  standalone: true,
  imports: [],
  templateUrl: './cepten-al-paraayal.html',
  styleUrl: './cepten-al-paraayal.css',
})
export class CeptenAlParaayal {

  constructor(private router: Router) {}

  anaMenuye(): void {
    sessionStorage.clear();
    this.router.navigate(['/']);
  }
}