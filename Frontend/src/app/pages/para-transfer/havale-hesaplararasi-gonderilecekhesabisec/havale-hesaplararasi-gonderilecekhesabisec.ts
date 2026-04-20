import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Hesap, HesapService } from '../../../services/hesap';

@Component({
  selector: 'app-havale-hesaplararasi-gonderilecekhesabisec',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './havale-hesaplararasi-gonderilecekhesabisec.html',
  styleUrl: './havale-hesaplararasi-gonderilecekhesabisec.css',
})
export class HavaleHesaplararasiGonderilecekhesabisec implements OnInit {

  hesaplar: Hesap[] = [];
  secilenHesap: Hesap | null = null;
  gonderenHesap: Hesap | null = null;
  yukleniyor = true;
  hata: string | null = null;

  constructor(
    private router: Router,
    private hesapService: HesapService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const kartNumara = sessionStorage.getItem('kartNumara');
    const gonderenHesapJson = sessionStorage.getItem('gonderenHesap');

    if (!kartNumara || !gonderenHesapJson) {
      this.hata = 'Oturum bulunamadı.';
      this.yukleniyor = false;
      this.router.navigate(['/login']);
      return;
    }

    this.gonderenHesap = JSON.parse(gonderenHesapJson);

    this.hesapService.musteriTumHesaplariGetir(kartNumara).subscribe({
      next: (response) => {
        this.hesaplar = response.data.filter(
          h => h.hesapNumara !== this.gonderenHesap!.hesapNumara
        );
        this.yukleniyor = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.hata = 'Hesaplar yüklenemedi.';
        this.yukleniyor = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  hesapSec(hesap: Hesap): void {
    this.secilenHesap = hesap;
    sessionStorage.setItem('aliciHesap', JSON.stringify(hesap));
    this.router.navigate(['/havale-hesaplararasi-tutar']);
  }

  geriDon(): void {
    this.router.navigate(['/havale-hesaplararasi-gonderecekhesabisec']);
  }

  anaMenuye(): void {
    this.router.navigate(['/atm']);
  }
}