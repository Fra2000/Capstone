import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './services/Account/auth.service'; // Assicurati di avere un AuthService per gestire il token

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken(); // Supponendo che l'AuthService abbia un metodo per ottenere il token

    if (token) {
      const modifiedReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`) // Aggiungi il token JWT nell'header Authorization
      });
      return next.handle(modifiedReq);
    }

    return next.handle(req);
  }
}
