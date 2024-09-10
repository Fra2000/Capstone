import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../interfaces/user';
import { LoginModel } from '../../interfaces/login-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'https://localhost:7097/api/Account';

  constructor(private http: HttpClient) { }

  register(user: User, imageFile: File | null): Observable<any> {
    const formData: FormData = new FormData();

    // Aggiungi i dati dell'utente al FormData
    formData.append('FirstName', user.firstName);
    formData.append('LastName', user.lastName);
    formData.append('Username', user.username);
    formData.append('Email', user.email);
    formData.append('Password', user.password);

    // Aggiungi l'immagine solo se presente
    if (imageFile) {
      formData.append('imageFile', imageFile);
    }

    return this.http.post<any>(`${this.baseUrl}/register`, formData);
  }

  login(credentials: LoginModel): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(`${this.baseUrl}/login`, credentials, { headers, withCredentials: true });
  }

  logout(): Observable<any> {
    return this.http.post<any>('https://localhost:7097/api/Account/logout', {});
  }

}
