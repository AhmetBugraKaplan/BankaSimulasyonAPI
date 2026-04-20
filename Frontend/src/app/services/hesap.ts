import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export interface Hesap {
    id: number;
    hesapNumara: string;
    hesapBakiye: number;
    musteriId: number;
    hesapTip?: string;
    paraBirimi?: string;
}

export interface BaseResponse<T> {
    islemBasariliMi: boolean;
    mesaj: string;
    data: T;
}

export interface HavaleTalebi {
    GonderenHesapNumara: string;
    AliciHesapNumara: string;
    GonderilenTutar: number;
    KartNumara: string;
}



@Injectable({
    providedIn: 'root',
})
export class HesapService {
    private hesapUrl = 'http://localhost:5032/api/Hesap';

    constructor(private http: HttpClient) { }


    musteriTumHesaplariGetir(kartNumara: string): Observable<BaseResponse<Hesap[]>> {
        return this.http.post<string>(
            `${this.hesapUrl}/MusteriTumHesaplariGetir`,
            { KartNumara: kartNumara },
            { responseType: 'text' as 'json' }
        ).pipe(
            map(raw => JSON.parse(raw) as BaseResponse<Hesap[]>)
        );
    }

    havaleYap(talep: HavaleTalebi): Observable<BaseResponse<any>> {
        return this.http.post<BaseResponse<any>>(
            `${this.hesapUrl}/HavaleYap`,
            talep
        )
    }

    hesapVarMi(hesapNumara: string): Observable<BaseResponse<boolean>> {
        return this.http.post<string>(
            `${this.hesapUrl}/HesapVarMi`,
            { HesapNumara: hesapNumara },
            { responseType: 'text' as 'json' }
        ).pipe(
            map(raw => JSON.parse(raw) as BaseResponse<boolean>)
        );
    }



}