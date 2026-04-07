import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export interface Hesap {
    id: number;
    hesapNumara: string;
    hesapTip: string;
    hesapBakiye: number;
    paraBirimi: string;
    musteriId: number;      
}

export interface BaseResponse<T> {
    islemBasariliMi: boolean;
    mesaj: string;
    data: T;
}



@Injectable({
    providedIn: 'root',
})
export class HesapService {
    private hesapUrl = 'http://localhost:5032/api/Hesap';

    constructor(private http: HttpClient) { }


    musteriTumHesaplariGetir(kartNumara: string): Observable<BaseResponse<Hesap[]>> {
        return this.http.post<BaseResponse<Hesap[]>>(
            `${this.hesapUrl}/MusteriTumHesaplariGetir`, 
            { KartNumara: kartNumara }
        );
    }
}