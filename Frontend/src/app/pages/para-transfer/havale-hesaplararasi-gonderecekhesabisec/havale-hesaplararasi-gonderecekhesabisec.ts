import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Hesap, HesapService } from '../../../services/hesap';

@Component({
  selector: 'app-havale-hesaplararasi-gonderecekhesabisec',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './havale-hesaplararasi-gonderecekhesabisec.html',
  styleUrl: './havale-hesaplararasi-gonderecekhesabisec.css',
})
export class HavaleHesaplararasiGonderecekhesabisec implements OnInit {

  hesaplar: Hesap[] = [];
  secilenHesap: Hesap | null = null;
  yukleniyor = true;
  hata: string | null = null;
  

  constructor(
    private router: Router,
    private hesapService: HesapService,
    private cdr: ChangeDetectorRef  
  ) { }

  ngOnInit(): void {
    const kartNumara = sessionStorage.getItem('kartNumara');

    if (!kartNumara) {
      this.hata = 'Oturum bulunamadı.';
      this.yukleniyor = false;
      this.router.navigate(['/login']);
      return;
    }

    this.hesapService.musteriTumHesaplariGetir(kartNumara).subscribe({
      next: (response) => {
        console.log('response:', response);
        console.log('response.data:', response.data);
        console.log('type:', typeof response);
        this.hesaplar = response.data;
        this.yukleniyor = false;
        this.cdr.detectChanges();  

      },
      error: (err) => {
        this.hata = 'Hesaplar yüklenemedi.';
        this.yukleniyor = false;
        console.error(err);
        this.cdr.detectChanges(); 
      }
    });
  }

  hesapSec(hesap: Hesap): void {
    this.secilenHesap = hesap;
    sessionStorage.setItem('gonderenHesap', JSON.stringify(hesap));
    this.router.navigate(['/havale-hesaplararasi-alacakhesabisec']);
  }

  geriDon(): void {
    this.router.navigate(['/havale-tipi']);
  }

  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}