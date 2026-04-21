import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseResponse } from './hesap';

@Injectable({
  providedIn: 'root',
})
export class Onay {

  private OnayUrl = 'http://localhost:5032/api/Onay';

  constructor(private http: HttpClient) {}

  onayKoduDogruMu(kod: string,telefonNumara:string): Observable<BaseResponse<boolean>> {
    return this.http.post<BaseResponse<boolean>>(
      `${this.OnayUrl}/OnayKoduDogruMu`,
            {telefonNumara:telefonNumara,
              kod:kod
            }
    )
  }

  onayKodUret(telefonNumara:string): Observable<BaseResponse<any>> {
    return this.http.post<BaseResponse<any>>(
      `${this.OnayUrl}/OnayKoduUret`,
            {telefonNumara:telefonNumara}
    )
  }

  

}
