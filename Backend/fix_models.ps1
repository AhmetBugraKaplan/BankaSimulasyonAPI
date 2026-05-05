$dir = ""c:\Users\bugra\Desktop\aspnetcore-dersleri\BankaSimulasyon\Backend\Models\Requests""
Get-ChildItem -Path $dir -Recurse -Filter *.cs | ForEach-Object {
    $content = Get-Content $_.FullName -Raw

    $content = $content -replace '(?m)^\s*\[Required.*?\]\s*\r?\n', ''
    $content = $content -replace '(?m)^\s*\[Length.*?\]\s*\r?\n', ''
    $content = $content -replace '(?m)^\s*\[RegularExpression.*?\]\s*\r?\n', ''
    $content = $content -replace '(?m)^\s*\[Range.*?\]\s*\r?\n', ''
    $content = $content -replace '(?m)^\s*\[StringLength.*?\]\s*\r?\n', ''
    $content = $content -replace '(?m)^\s*\[Phone.*?\]\s*\r?\n', ''

    # KartNumara
    $content = [regex]::Replace($content, '(\s*)(public\s+string\s+(?:KartNumara|GonderenKartNo|GonderenKartNumara)\b)', '$1[Required(ErrorMessage=""Kart numarasý zorunludur"")]$1[RegularExpression(@""^\d{4}$"", ErrorMessage=""Kart numarasý 4 haneli olmalýdýr"")]$1$2')
    
    # Sifre
    $content = [regex]::Replace($content, '(\s*)(public\s+string\s+(?:KartSifre|YeniKartSifre)\b)', '$1[Required(ErrorMessage=""Þifre zorunludur"")]$1[RegularExpression(@""^\d{3}$"", ErrorMessage=""Þifre 3 haneli olmalýdýr"")]$1$2')

    # HesapNumara
    $content = [regex]::Replace($content, '(\s*)(public\s+string\s+(?:HesapNumara|AliciHesapNumara|GonderenHesapNumara)\b)', '$1[Required(ErrorMessage=""Hesap numarasý zorunludur"")]$1[RegularExpression(@""^\d{3}$"", ErrorMessage=""Hesap numarasý 3 haneli olmalýdýr"")]$1$2')

    # TelefonNumara
    $content = [regex]::Replace($content, '(\s*)(public\s+string\s+(?:TelefonNumara|AliciTelNo|GonderenTelNo)\b)', '$1[Required(ErrorMessage=""Telefon numarasý zorunludur"")]$1[RegularExpression(@""^0\d{10}$"", ErrorMessage=""Telefon numarasý 0 ile baþlamalý ve 11 haneli olmalýdýr"")]$1$2')

    # Onay Kod
    $content = [regex]::Replace($content, '(\s*)(public\s+string\s+(?:Kod)\b)', '$1[Required(ErrorMessage=""Onay kodu zorunludur"")]$1[RegularExpression(@""^\d{4}$"", ErrorMessage=""Onay kodu 4 haneli olmalýdýr"")]$1$2')

    # Tutar
    $content = [regex]::Replace($content, '(\s*)(public\s+(?:int|decimal)\s+(?:Tutar|CekilecekTutar|GonderilecekTutar|GonderilenTutar|ParaMiktari)\b)', '$1[Required(ErrorMessage=""Tutar zorunludur"")]$1[Range(10, int.MaxValue, ErrorMessage=""Tutar en az 10 TL olmalýdýr"")]$1$2')

    # Limit
    $content = [regex]::Replace($content, '(\s*)(public\s+(?:int|decimal)\s+(?:KartGunlukLimit|YeniKartLimit)\b)', '$1[Required(ErrorMessage=""Limit zorunludur"")]$1[Range(10, int.MaxValue, ErrorMessage=""Limit en az 10 TL olmalýdýr"")]$1$2')

    # Id
    $content = [regex]::Replace($content, '(\s*)(public\s+int\s+(?:AtmId|KullaniciId|MusteriId|Id)\b)', '$1[Required(ErrorMessage=""ID zorunludur"")]$1[Range(1, int.MaxValue, ErrorMessage=""Geçerli bir ID giriniz"")]$1$2')

    # TCKN
    $content = [regex]::Replace($content, '(\s*)(public\s+string\s+(?:AliciTckNO)\b)', '$1[Required(ErrorMessage=""TCKN zorunludur"")]$1[RegularExpression(@""^\d{11}$"", ErrorMessage=""TCKN 11 haneli olmalýdýr"")]$1$2')

    # Isim / Soyisim
    $content = [regex]::Replace($content, '(\s*)(public\s+string\s+(?:Isim|Soyisim)\b)', '$1[Required(ErrorMessage=""Bu alan zorunludur"")]$1$2')

    if ($content -notmatch ""using System.ComponentModel.DataAnnotations;"") {
        $content = ""using System.ComponentModel.DataAnnotations;
"" + $content
    }

    Set-Content -Path $_.FullName -Value $content -Encoding UTF8
}
