import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UpdateCartItem } from '../interfaces/updateCartItem'; // Usa la tua nuova interfaccia
import { CartItem } from '../interfaces/cartItem';
import { Cart } from '../interfaces/cart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'https://localhost:7097/api/Cart'; // L'endpoint del carrello

  constructor(private http: HttpClient) { }

  // Aggiunge o aggiorna un libro nel carrello
  addToCart(cartItem: UpdateCartItem): Observable<CartItem> {
    return this.http.post<CartItem>(`${this.apiUrl}`, cartItem);
  }

  getCart(): Observable<Cart> {
    return this.http.get<Cart>(this.apiUrl); // Richiesta GET per ottenere il carrello
  }

  removeCartItem(userBookId: number, quantity: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${userBookId}?quantity=${quantity}`);
  }

  purchase(): Observable<any> {
    return this.http.post(`${this.apiUrl}/complete-purchase`, {}); // Cambia l'URL se necessario
  }

}
