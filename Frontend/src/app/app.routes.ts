import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Atm } from './pages/atm/atm';
import { SifreDegistir } from './pages/sifre-degistir/sifre-degistir';
import { ParaCek } from './pages/para-cek/para-cek';
import { LimitGoruntule } from './pages/limit-goruntule/limit-goruntule';
import { ParaTransfer } from './pages/para-transfer/para-transfer';
import { HavaleTipi } from './pages/para-transfer/havale-tipi/havale-tipi';
import { HesapSec } from './pages/para-transfer/hesap-sec/hesap-sec';
import { AliciHesapGiris } from './pages/para-transfer/alici-hesap-giris/alici-hesap-giris';
import { TransferTutar } from './pages/para-transfer/transfer-tutar/transfer-tutar';
import { IslemOnaylandi } from './pages/para-transfer/islem-onaylandi/islem-onaylandi';
import { HavaleHesaplararasiGonderilecekhesabisec } from './pages/para-transfer/havale-hesaplararasi-gonderilecekhesabisec/havale-hesaplararasi-gonderilecekhesabisec';
import { HavaleHesaplararasiGonderecekhesabisec } from './pages/para-transfer/havale-hesaplararasi-gonderecekhesabisec/havale-hesaplararasi-gonderecekhesabisec';


export const routes: Routes = [
    { path: '', component: Login },
    { path: 'atm', component: Atm },
    { path: 'sifre-degistir', component: SifreDegistir },
    { path: 'para-cek', component: ParaCek },
    { path: 'limit-goruntule', component: LimitGoruntule },
    { path: 'para-transfer', component: ParaTransfer },
    { path: 'havale-tipi', component: HavaleTipi },
    { path: 'hesap-sec', component: HesapSec },
    { path: 'alici-hesap-giris', component: AliciHesapGiris },
    { path: 'transfer-tutar', component: TransferTutar },
    { path: 'islem-onaylandi', component: IslemOnaylandi },
    { path: 'havale-hesaplararasi-gonderilecekhesabisec', component: HavaleHesaplararasiGonderilecekhesabisec },
    { path: 'havale-hesaplararasi-gonderecekhesabisec', component: HavaleHesaplararasiGonderecekhesabisec },
    { path: 'havale-hesaplararasi-tutar', component: TransferTutar },
    { path: 'havale-hesaplararasi-alacakhesabisec', component: HavaleHesaplararasiGonderilecekhesabisec }
];
