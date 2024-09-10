import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root' // Questo rende il servizio disponibile in tutta l'applicazione
})
export class AuthService {
  private baseUrl = 'https://localhost:7097/api/Account';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());

  constructor(private http: HttpClient) {}

  // Controlla se il token JWT esiste nel localStorage
  private hasToken(): boolean {
    return !!localStorage.getItem('jwtToken');
  }

  // Restituisce lo stato corrente dell'autenticazione come Observable
  isAuthenticated(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
  }

  // Login: salva il token e aggiorna lo stato di autenticazione
  login(credentials: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/login`, credentials).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem('jwtToken', response.token);
          this.isAuthenticatedSubject.next(true); // Aggiorna lo stato di autenticazione a true
        }
      })
    );
  }

  // Logout: rimuove il token JWT dal localStorage e aggiorna lo stato di autenticazione
  logout(): void {
    localStorage.removeItem('jwtToken');
    this.isAuthenticatedSubject.next(false); // Aggiorna lo stato di autenticazione a false
  }

  // Metodo per ottenere il token JWT dal localStorage
  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  // Metodo di registrazione per creare un nuovo utente con un file immagine opzionale
  register(user: any, imageFile: File | null): Observable<any> {
    const formData: FormData = new FormData();

    // Aggiungi i dati dell'utente al FormData
    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('username', user.username);
    formData.append('email', user.email);
    formData.append('password', user.password);

    // Aggiungi l'immagine solo se presente
    if (imageFile) {
      formData.append('imageFile', imageFile);
    }

    return this.http.post<any>(`${this.baseUrl}/register`, formData);
  }
}
