import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { forkJoin, timer } from 'rxjs';
import { Hesap } from '../../../models/hesap.model';
import { HesapService } from '../../../services/hesap';

@Component({
  selector: 'app-alici-hesap-giris',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './alici-hesap-giris.html',
  styleUrl: './alici-hesap-giris.css',
})
export class AliciHesapGiris implements OnInit {

  secilenHesap: Hesap | null = null;
  aliciHesapNumara: string = '';
  hataMesaji: string = '';
  yukleniyor: boolean = false;

  constructor(
    private router: Router,
    private hesapService: HesapService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const data = sessionStorage.getItem('secilenHesap');
    if (!data) {
      this.router.navigate(['/hesap-sec']);
      return;
    }
    this.secilenHesap = JSON.parse(data);
  }

  devam(): void {
    this.hataMesaji = '';

    if (!this.aliciHesapNumara.trim()) {
      this.hataMesaji = 'Lütfen alıcı hesap numarasını giriniz.';
      return;
    }

    if (this.aliciHesapNumara.trim() === this.secilenHesap?.hesapNumara) {
      this.hataMesaji = 'Kendi hesabınıza transfer yapamazsınız.';
      return;
    }

    this.yukleniyor = true;
    this.cdr.detectChanges();

    forkJoin({
      apiCevabi: this.hesapService.hesapVarMi(this.aliciHesapNumara.trim()),
      beklemeSuresi: timer(1000)
    }).subscribe({
      next: (sonuc) => {
        this.yukleniyor = false;
        this.cdr.detectChanges();
        if (sonuc.apiCevabi.islemBasariliMi) {
          sessionStorage.setItem('aliciHesapNumara', this.aliciHesapNumara.trim());
          this.router.navigate(['/transfer-tutar']);
        } else {
          this.hataMesaji = 'Girdiğiniz hesap numarasına ait hesap bulunamadı.';
        }
      },
      error: () => {
        this.yukleniyor = false;
        this.hataMesaji = 'Sunucu ile iletişim kurulamadı.';
        this.cdr.detectChanges();
      }
    });
  }

  geriDon(): void {
    this.router.navigate(['/hesap-sec']);
  }

  anaMenuye(): void {
    sessionStorage.removeItem('secilenHesap');
    sessionStorage.removeItem('aliciHesapNumara');
    this.router.navigate(['/atm']);
  }
}