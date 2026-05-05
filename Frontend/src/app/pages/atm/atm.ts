import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-atm',
  imports: [],
  templateUrl: './atm.html',
  styleUrl: './atm.css',
})
export class Atm {


  constructor(private router: Router, private http: HttpClient) { }




  paraCek() {
    this.router.navigate(['/para-cek']);
  }

  limitGoruntule() {
    this.router.navigate(['/limit-goruntule'])
  }

  paraTransfer() {
    this.router.navigate(['/para-transfer'])
  }

  cebeParaGonder(): void {
    this.router.navigate(['/cebe-gonder-tc-giris']);
  }

  cikisYap() {
    this.http.post('http://localhost:5032/api/Kart/CikisYap', {}).subscribe({
      complete: () => {
        sessionStorage.clear();
        localStorage.clear();
        this.router.navigate(['/']);
      },
      error: () => {
        sessionStorage.clear();
        localStorage.clear();
        this.router.navigate(['/']);
      }
    });
  }
}