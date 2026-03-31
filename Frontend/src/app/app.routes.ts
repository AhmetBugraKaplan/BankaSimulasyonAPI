import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Atm } from './pages/atm/atm';
import { SifreDegistir } from './pages/sifre-degistir/sifre-degistir';
import { ParaCek } from './pages/para-cek/para-cek';
import { LimitGoruntule } from './pages/limit-goruntule/limit-goruntule';

export const routes: Routes = [
    {path: '',component:Login},
    {path: 'atm',component:Atm},
    {path: 'sifre-degistir', component:SifreDegistir},
    {path: 'para-cek',component:ParaCek},
    {path: 'limit-goruntule',component: LimitGoruntule}
];
