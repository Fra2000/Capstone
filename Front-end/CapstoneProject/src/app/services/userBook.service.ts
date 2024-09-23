import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserBookStatus } from '../interfaces/UserBookStatus';

@Injectable({
  providedIn: 'root',
})
export class UserBookService {
  private apiUrl = 'https://localhost:7097/api/BookStatus'; // Assicurati che l'URL sia corretto

  constructor(private http: HttpClient) { }

  getUserBookStatuses(): Observable<UserBookStatus[]> {
    return this.http.get<UserBookStatus[]>(`${this.apiUrl}/user-statuses`);
  }

  updateBookStatus(bookId: number, newStatus: string, currentPage?: number): Observable<any> {
    let url = `${this.apiUrl}/update-status?bookId=${bookId}&newStatus=${newStatus}`;

    // Aggiungi `currentPage` alla URL solo se lo stato è "In Corso" e `currentPage` è definito
    if (newStatus === 'In Corso' && currentPage !== undefined && currentPage !== null) {
      url += `&currentPage=${currentPage}`;
    }

    return this.http.put(url, {});
  }




}
