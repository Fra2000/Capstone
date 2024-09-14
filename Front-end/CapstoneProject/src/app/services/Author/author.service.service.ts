import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthorRead } from '../../interfaces/AuthorRead'; // Assicurati di creare questa interfaccia
import { Book } from '../../interfaces/Book';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private apiUrl = 'https://localhost:7097/api/Author';

  constructor(private http: HttpClient) { }

  getAllAuthors(): Observable<AuthorRead[]> {
    return this.http.get<AuthorRead[]>(this.apiUrl);
  }

  getAuthorById(authorId: number): Observable<AuthorRead> {
    return this.http.get<AuthorRead>(`${this.apiUrl}/${authorId}`);
  }

  createAuthor(authorData: AuthorRead): Observable<AuthorRead> {
    return this.http.post<AuthorRead>(this.apiUrl, authorData);
  }

  updateAuthor(authorId: number, authorData: AuthorRead): Observable<AuthorRead> {
    return this.http.put<AuthorRead>(`${this.apiUrl}/${authorId}`, authorData);
  }

  deleteAuthor(authorId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${authorId}`);
  }

  getAuthorImagePath(relativePath: string): string {
    if (!relativePath) {
      // Fornisci un'immagine di default se non presente
      return 'https://localhost:7097/images/Author/default.jpg';
    }
    return `https://localhost:7097/${relativePath}`;
  }

  handleImageError(event: any) {
    event.target.src = 'https://localhost:7097/images/Author/default.jpg'; // Immagine di fallback
  }

  getBooksByAuthorId(authorId: number): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/${authorId}`);
  }

}
