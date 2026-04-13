import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Hesap } from '../../../models/hesap.model';

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

  constructor(private router: Router) {}

  ngOnInit(): void {
    const data = sessionStorage.getItem('secilenHesap');
    if (!data) {
      this.router.navigate(['/hesap-sec']);
      return;
    }
    this.secilenHesap = JSON.parse(data);
  }

  devam(): void {
    if (!this.aliciHesapNumara.trim()) {
      this.hataMesaji = 'Lütfen alıcı hesap numarasını giriniz.';
      return;
    }
    sessionStorage.setItem('aliciHesapNumara', this.aliciHesapNumara.trim());
    this.router.navigate(['/transfer-tutar']);
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