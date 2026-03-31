import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Kart } from '../../services/kart';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-para-cek',
  imports: [FormsModule,CommonModule],
  templateUrl: './para-cek.html',
  styleUrl: './para-cek.css',
})
export class ParaCek {
  cekilecekTutar: number = 0;
  sonuclar: any[] = [];
  hataMesaji: string = '';

constructor(private kartService:Kart,private router:Router,private cdr: ChangeDetectorRef){}

paraCek(){
  const token = localStorage.getItem('token');

  if(!token){
    this.router.navigate(['/']);
    return;
  }


  this.kartService.ParaCek(this.cekilecekTutar).subscribe(sonuc=> {
    if(sonuc.islemBasariliMi){
      this.sonuclar = [...sonuc.data];
      this.cdr.detectChanges(); 
    }else{
      this.hataMesaji = sonuc.mesaj;
    }
  });
}

geriDon() {
    this.router.navigate(['/atm']);
  }

}


