import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../../interfaces/Book';
import { BookRead } from '../../interfaces/BookRead';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7097/api/Book';

  constructor(private http: HttpClient) { }

  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }

  getCoverImagePath(relativePath: string): string {
    if (!relativePath) {
      return 'https://localhost:7097/images/Book/default.jpg'; // immagine di default se non presente
    }
    return `https://localhost:7097/${relativePath}`;
  }

  handleImageError(event: any) {
    event.target.src = 'https://localhost:7097/images/Book/default.jpg';
  }

  getBookById(bookId: number): Observable<BookRead> {
    return this.http.get<BookRead>(`${this.apiUrl}/${bookId}`);
  }

  updateBook(bookId: number, bookData: BookRead): Observable<BookRead> {
    return this.http.put<BookRead>(`${this.apiUrl}/${bookId}`, bookData);
  }



  deleteBook(bookId: number): Observable<BookRead> {
    return this.http.delete<BookRead>(`${this.apiUrl}/${bookId}`);
  }

  getBooksByAuthorId(authorId: number): Observable<Book[]> {
    return this.http.get<Book[]>(`https://localhost:7097/api/Author/${authorId}`);
  }




}
