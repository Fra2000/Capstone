import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Genre } from '../../interfaces/Genre';
import { Book } from '../../interfaces/Book';

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
    console.log('Fetching books for genre ID:', genreId); // Log dell'ID del genere usato nella richiesta
    return this.http.get<Book[]>(`${this.apiUrl}/${genreId}`);
  }

}
