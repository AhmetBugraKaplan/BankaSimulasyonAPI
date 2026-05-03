import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  // 1. localStorage'dan token'ı al
  const token = sessionStorage.getItem('token');
  const router = inject(Router)

  // 2. Token varsa isteğe ekle
  if (token) {
    // req.clone() → mevcut isteğin bir kopyasını oluşturur
    // HTTP istekleri immutable'dır (değiştirilemez), bu yüzden klonlayıp değiştiriyoruz
    const clonedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`  // burada kopya isteğimize token ekliyoruz
        //normalde kopya istedğimiz şöyle bir yapıydı:   POST /ParaCek + Body: {tutar:500} + 
        // Buraya birde  Authorization: Bearer eyJhbGci ekliyoruz     

      }
    });

    // Klonlanmış isteği (token eklenmiş) gönder
    return next(clonedReq).pipe(
      catchError(error => {
        if (error.status === 401) {
          sessionStorage.clear();
          localStorage.clear();
          router.navigate(['/']);
        }
        return throwError(() => error);
      })
    );
  }

  // 3. Token yoksa isteği olduğu gibi gönder
  return next(req);
};