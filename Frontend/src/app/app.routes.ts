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

// Kartsız Havale
import { TelNoGir } from './pages/kartsiz-islem/kartsiz-havale/tel-no-gir/tel-no-gir';
import { SmsOnayKoduGir } from './pages/kartsiz-islem/kartsiz-havale/sms-onay-kodu-gir/sms-onay-kodu-gir';
import { AliciHesapNoGir } from './pages/kartsiz-islem/kartsiz-havale/alici-hesap-no-gir/alici-hesap-no-gir';
import { GonderilecekTutarGir } from './pages/kartsiz-islem/kartsiz-havale/gonderilecek-tutar-gir/gonderilecek-tutar-gir';
import { KartsizIslemOnaylandi } from './pages/kartsiz-islem/kartsiz-havale/kartsiz-islem-onaylandi/kartsiz-islem-onaylandi';

// Kartsız İslem Menu
import { KartsizIslemMenu } from './pages/kartsiz-islem/kartsiz-islem-menu/kartsiz-islem-menu';

// Cebe Para Gönder
import { CebeGonderTcGiris } from './pages/kartsiz-islem/cebe-para-gonder/cebe-gonder-tc-giris/cebe-gonder-tc-giris';
import { CebeGonderKendiTelnoGiris } from './pages/kartsiz-islem/cebe-para-gonder/cebe-gonder-tc-giris/cebe-gonder-kendi-telno-giris/cebe-gonder-kendi-telno-giris';
import { CebeGonderGonderilecektutargiris } from './pages/kartsiz-islem/cebe-para-gonder/cebe-gonder-gonderilecektutargiris/cebe-gonder-gonderilecektutargiris';

// Cepten Al
import { CeptenAlTcGiris } from './pages/kartsiz-islem/cepten-al/cepten-al-tc-giris/cepten-al-tc-giris';
import { CeptenAlCepnoGiris } from './pages/kartsiz-islem/cepten-al/cepten-al-cepno-giris/cepten-al-cepno-giris';
import { CeptenAlGonderenCepnoGiris } from './pages/kartsiz-islem/cepten-al/cepten-al-gonderen-cepno-giris/cepten-al-gonderen-cepno-giris';
import { CeptenAlTutarbilgisiGiris } from './pages/kartsiz-islem/cepten-al/cepten-al-tutarbilgisi-giris/cepten-al-tutarbilgisi-giris';
import { CeptenAlSmsonayGiris } from './pages/kartsiz-islem/cepten-al/cepten-al-smsonay-giris/cepten-al-smsonay-giris';
import { CeptenAlOnay } from './pages/kartsiz-islem/cepten-al/cepten-al-onay/cepten-al-onay';
import { CeptenAlParaayal } from './pages/kartsiz-islem/cepten-al/cepten-al-paraayal/cepten-al-paraayal';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'atm', component: Atm },
  { path: 'sifre-degistir', component: SifreDegistir },
  { path: 'para-cek', component: ParaCek },
  { path: 'limit-goruntule', component: LimitGoruntule },

  // Para Transfer
  { path: 'para-transfer', component: ParaTransfer },
  { path: 'havale-tipi', component: HavaleTipi },
  { path: 'hesap-sec', component: HesapSec },
  { path: 'alici-hesap-giris', component: AliciHesapGiris },
  { path: 'transfer-tutar', component: TransferTutar },
  { path: 'islem-onaylandi', component: IslemOnaylandi },
  { path: 'havale-hesaplararasi-gonderilecekhesabisec', component: HavaleHesaplararasiGonderilecekhesabisec },
  { path: 'havale-hesaplararasi-gonderecekhesabisec', component: HavaleHesaplararasiGonderecekhesabisec },
  { path: 'havale-hesaplararasi-tutar', component: TransferTutar },
  { path: 'havale-hesaplararasi-alacakhesabisec', component: HavaleHesaplararasiGonderilecekhesabisec },

  // Kartsız Havale
  { path: 'tel-no-gir', component: TelNoGir },
  { path: 'sms-onay-kodu-gir', component: SmsOnayKoduGir },
  { path: 'alici-hesap-no-gir', component: AliciHesapNoGir },
  { path: 'gonderilecek-tutar-gir', component: GonderilecekTutarGir },
  { path: 'kartsiz-islem-onaylandi', component: KartsizIslemOnaylandi },

  // Kartsız İşlem Menu
  { path: 'kartsiz-islem-menu', component: KartsizIslemMenu },

  // Cebe Para Gönder
  { path: 'cebe-gonder-tc-giris', component: CebeGonderTcGiris },
  { path: 'cebe-gonder-kendi-telno-giris', component: CebeGonderKendiTelnoGiris },
  { path: 'cebe-gonder-gonderilecektutargiris', component: CebeGonderGonderilecektutargiris },

  // Cepten Al
  { path: 'cepten-al-tc-giris', component: CeptenAlTcGiris },
  { path: 'cepten-al-cepno-giris', component: CeptenAlCepnoGiris },
  { path: 'cepten-al-gonderen-cepno-giris', component: CeptenAlGonderenCepnoGiris },
  { path: 'cepten-al-tutarbilgisi-giris', component: CeptenAlTutarbilgisiGiris },
  { path: 'cepten-al-smsonay-giris', component: CeptenAlSmsonayGiris },
  { path: 'cepten-al-onay', component: CeptenAlOnay },
  { path: 'cepten-al-paraayal', component: CeptenAlParaayal },
];