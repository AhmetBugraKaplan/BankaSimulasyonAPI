import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-atm',
  imports: [],
  templateUrl: './atm.html',
  styleUrl: './atm.css',
})
export class Atm {


constructor(private router:Router){}

  paraCek(){
    this.router.navigate(['/para-cek']);
  }

  limitGoruntule(){
    this.router.navigate(['/limit-goruntule'])
  }

}
