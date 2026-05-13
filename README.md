
**Vakıfbank** ATM ekibinde gerçekleştirdiğim bu proje, gerçek dünya standartlarında bir ATM uygulamasının tüm uçtan uca (End-to-End) süreçlerini simüle eden, güçlü bir **ASP.NET Core Web API** arka planına ve modern bir **Angular** arayüzüne sahip Full-Stack bankacılık uygulamasıdır. Mimarisi, güvenlik önlemleri ve iş mantığı (Business Logic) kurumsal ölçekli bir bankacılık altyapısı örnek alınarak tasarlanmıştır.

---

## 📸 Ekran Görüntüleri

### 1. Sisteme Giriş (Login) & Kartsız İşlemler
Kullanıcılar kart numarası, şifre ve fiziksel ATM kimliği (ATM ID) ile güvenli giriş yapabilirler.
![Login Ekranı]<img width="1907" height="904" alt="AtmSimulasyonGirişEkrani" src="https://github.com/user-attachments/assets/8a3a00b2-4266-4929-af12-250f608cb57b" />


### 2. Ana Menü
Sisteme giriş yapıldıktan sonra kullanıcıyı karşılayan, tüm işlemlerin yönetildiği merkezi menü.
![Ana Menü]<img width="1900" height="890" alt="AtmSimulasyonIslemMenusu" src="https://github.com/user-attachments/assets/d1cae2e5-fa8d-480c-9ef5-b24c643ceadd" />


### 3. Hesap Seçimi & Transferler
Havale (Başka Hesaba) ve Virman (Kendi Hesaplarım Arası) işlemleri için dinamik hesap seçim ekranı.
![Hesap Seçimi]<img width="1868" height="866" alt="AtmSimulasyonGonderilecekTutar" src="https://github.com/user-attachments/assets/22a9299f-1f1a-46da-99e8-04344889a1f7" />


---

## 🚀 Öne Çıkan Özellikler (Features)

### 🧠 Akıllı Para Çekme Algoritması (Smart Withdrawal)
Kullanıcının talep ettiği tutarı, ATM kasasındaki mevcut banknot (küpür) durumuna göre en uygun şekilde veren (veya bozukluk kalmadığında uyaran) **Optimizasyon Algoritması**.

### 🔒 Üst Düzey Güvenlik (Advanced Security)
- **IpControl Middleware & X-Forwarded-For:** Oturum çalma (Session Hijacking) saldırılarına karşı token içindeki IP ile istek yapılan IP'nin eşleşme kontrolü.
- **Rate Limiting:** Kaba kuvvet (Brute Force) ve DDoS saldırılarını engellemek için IP bazlı hız sınırlandırması.
- **Global Exception Handling:** Uygulama genelinde oluşan tüm hataların tek bir merkezden yakalanıp, istemciye güvenli ve standartlaştırılmış JSON formatında iletilmesi.
- **JWT (JSON Web Token):** Rol ve IP tabanlı, süre kısıtlamalı güvenli yetkilendirme altyapısı.

### 🔄 Gelişmiş İşlem Yönetimi (Transaction & UoW)
- **Unit of Work Pattern (TransactionScope):** "Cebe Para Gönder" veya "Havale" gibi çok adımlı veri tabanı işlemlerinde hata olması durumunda tüm işlemlerin geri alınması (Rollback) garantisi (Atomicity).
- **Virman & Havale Ayrımı:** Aynı API endpoint'i (HavaleYap) üzerinden, gönderilen bayraklar (flags) ile kendi hesapların arası (Virman) ve başkasının hesabına (Havale) işlemlerin güvenlik protokollerinin izole edilmesi.
- **Kartsız İşlemler:** SMS Onay kodları üzerinden yürüyen Session tabanlı (SessionStorage), ardışık işlemlere olanak tanıyan State Management.

---

## 🛠️ Kullanılan Teknolojiler (Tech Stack)

### Backend
- **Framework:** .NET 8 (ASP.NET Core Web API)
- **Architecture:** N-Tier Architecture (Repository & Service Patterns)
- **Database:** SQL Server (Entity Framework Core & ADO.NET / Stored Procedures)
- **Security:** JWT, Custom Middlewares (IP Control, Exception, Rate Limiting), BCrypt (Password Hashing)
- **Testing:** xUnit & Moq (Unit Testing)

### Frontend
- **Framework:** Angular 17+ (Standalone Components)
- **Styling:** Vanilla CSS (Modern UI/UX, Glassmorphism, Animations)
- **State Management:** RxJS, SessionStorage
- **Architecture:** BFF (Backend for Frontend) konseptine uygun Client-Side Routing.

---

## 🏗️ Mimari Yaklaşım

Proje, frontend'in (Angular) gereksiz yükten kurtarılıp sadece kullanıcı deneyimine (UX) odaklandığı, karmaşık iş kurallarının (Business Rules) ve veritabanı manipülasyonlarının Backend (API) tarafında izole edildiği kurumsal bir mimariyle geliştirilmiştir.

- **BFF (Backend for Frontend) Vizyonu:** Frontend sadece ihtiyacı olan veriyi alır (Over-fetching önlenmiştir). Veri maskeleme ve güvenlik kuralları Backend servislerinde işlenir.
- **Type-Safety:** Uygulama genelinde `object` anti-pattern'ından kaçınılmış; `ApiResponse<T>`, `LoginResponse` gibi kesin tipli (Strongly-typed) DTO'lar kullanılarak veri tutarlılığı sağlanmıştır.

---

## 💻 Kurulum ve Çalıştırma

### 1. Veritabanı Kurulumu
- Backend dizinindeki `appsettings.json` dosyasını açarak `ConnectionStrings` içerisine kendi SQL Server bağlantı dizenizi yazın.
- Package Manager Console üzerinden (veya terminalden) veritabanını oluşturun:
```bash
Update-Database
```

### 2. Backend'i Başlatma
Backend dizinine giderek projeyi çalıştırın:
```bash
cd Backend
dotnet run
```
API varsayılan olarak `http://localhost:5032` üzerinde çalışacaktır.

### 3. Frontend'i Başlatma
Frontend bağımlılıklarını yükleyip projeyi ayağa kaldırın:
```bash
cd Frontend
npm install
npm start
```
Tarayıcınızdan `http://localhost:4200` adresine giderek ATM simülasyonunu kullanmaya başlayabilirsiniz.

---

## 👨‍💻 Geliştirici

**[Ahmet Buğra Kaplan/]**
Bu proje, ileri seviye bankacılık yazılım süreçlerinin analiz edilip uygulanması amacıyla geliştirilmiştir. Staj ve mülakat süreçlerinde teknik bir referans olarak sunulmuştur.
