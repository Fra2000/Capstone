import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Genre } from '../interfaces/Genre';
import { Book } from '../interfaces/Book';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private apiUrl = 'https://localhost:7097/api/Genre';

  constructor(private http: HttpClient) { }


  getAllGenres(): Observable<Genre[]> {
    return this.http.get<Genre[]>(this.apiUrl);
  }

  getBooksByGenreId(genreId: number): Observable<Book[]> {
    console.log('Fetching books for genre ID:', genreId);
    return this.http.get<Book[]>(`${this.apiUrl}/${genreId}`);
  }

  createGenre(genreData: { GenreName: string }): Observable<Genre> {
    return this.http.post<Genre>(`${this.apiUrl}`, genreData);
  }

  updateGenre(genreId: number, genreData: { GenreName: string }): Observable<Genre> {
    return this.http.put<Genre>(`${this.apiUrl}/${genreId}`, genreData);
  }

  getGenreById(genreId: number): Observable<Genre> {
    return this.http.get<Genre>(`${this.apiUrl}/${genreId}`);
  }

  deleteGenre(genreId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${genreId}`);
  }



}
