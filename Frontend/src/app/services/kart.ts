import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


//Injectable bu servis başka yerlere inject edilbeilir demek yani bu servisi başka bir componentte ya da servisde kullanabiliriz.
@Injectable({
  //root  = Bu servis uygulamanın her yerinden kullanılabilir demek
  providedIn: 'root',
})


export class Kart {

  private apiUrl = 'http://localhost:5032/api/Kart';

  constructor(private http: HttpClient) { }

  kartDogrula(kartNumara: string, kartSifre: string, atmId: number): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}/KartDogrula`,
      {
        KartNumara: kartNumara, //Bu kısımdaki 1. isimlendirme Backenddeki requestteki isimlendirme ile aynı olmalı. 
        KartSifre: kartSifre,
        AtmId: atmId
      }
    );
  }


  kartSifreGuncelle(kartNumara: string, yeniSifre: string): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}/KartSifreGuncelle`,
      {
        YeniKartSifre: yeniSifre,
        KartNumara: kartNumara
      }
    );
  }
  //postu badyden gonder
  //postman kullan
  /*
  kartYanlisGirisGetir(kartNumara: string): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}/KartYanlisGirisSayisiGetir?kartNumara=${kartNumara}`,
      {}
    );
  }
    */

  ParaCek(cekilecekTutar: number): Observable<any> {
    const kartNumara = sessionStorage.getItem('kartNumara');
    const atmId = Number(sessionStorage.getItem('atmId'));
    return this.http.post<any>(
      `${this.apiUrl}/ParaCek`,
      {
        CekilecekTutar: cekilecekTutar,
        kartNumara: kartNumara,
        AtmId: atmId
      }
    );
  }

  kartKalanLimitGetir(): Observable<any> {
    const kartNumara = sessionStorage.getItem('kartNumara');


    return this.http.post<any>(
      `${this.apiUrl}/KartKalanLimitGetir`, 
      { 
        KartNumara: kartNumara
      });
  }

 

}