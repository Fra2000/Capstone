import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthorRead } from '../../interfaces/Author'; // Assicurati di creare questa interfaccia
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

  createAuthor(authorData: FormData): Observable<AuthorRead> {
    return this.http.post<AuthorRead>(`${this.apiUrl}`, authorData);
  }

  updateAuthor(authorId: number, authorData: FormData): Observable<AuthorRead> {
    return this.http.put<AuthorRead>(`${this.apiUrl}/${authorId}`, authorData);
  }


  deleteAuthor(authorId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${authorId}`);
  }


  getAuthorImagePath(relativePath: string): string {
    if (!relativePath) {
      return 'https://localhost:7097/images/Author/default.jpg';
    }
    return `https://localhost:7097/${relativePath}`;
  }

  handleImageError(event: any) {
    event.target.src = 'https://localhost:7097/images/Author/default.jpg'; // Immagine di fallback
  }

  getBooksByAuthorId(authorId: number): Observable<Book[]> {
    // Assicurati che l'endpoint sia corretto per ottenere i libri di un autore
    return this.http.get<Book[]>(`${this.apiUrl}/${authorId}/books`);
  }
}
