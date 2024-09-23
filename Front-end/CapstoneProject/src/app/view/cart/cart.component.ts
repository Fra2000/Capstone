import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { BookService } from '../../services/Book/book.service';
import { Cart } from '../../interfaces/cart';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cart: Cart | null = null;

  constructor(private cartService: CartService, private bookService: BookService, private router: Router) { }

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.cartService.getCart().subscribe(
      (cart: Cart) => {
        this.cart = cart;
        this.calculateCartTotal(); // Calcola il totale del carrello inizialmente
      },
      (error) => {
        console.error('Errore nel caricamento del carrello:', error);
      }
    );
  }

  // Funzione per ottenere l'URL dell'immagine
  getCoverImagePath(relativePath: string): string {
    return this.bookService.getCoverImagePath(relativePath);
  }

  // Gestione errore immagine
  handleImageError(event: any): void {
    event.target.src = this.bookService.getCoverImagePath('images/Book/default.jpg');
  }

  // Aggiorna il totale per un singolo articolo e ricalcola il totale del carrello
  updateTotal(item: any): void {
    item.totalPrice = item.quantity * item.pricePerUnit;
    this.calculateCartTotal();
  }

  // Ricalcola il totale del carrello
  calculateCartTotal(): void {
    if (this.cart) {
      this.cart.cartTotal = this.cart.cartItems.reduce((acc, item) => acc + item.totalPrice, 0);
    }
  }

  deleteFromCart(item: any): void {
    this.cartService.removeCartItem(item.userBookId, 1).subscribe(() => {
      // Rimuovi completamente l'articolo dal carrello
      this.cart!.cartItems = this.cart!.cartItems.filter(cartItem => cartItem.userBookId !== item.userBookId);

      // Ricalcola il totale del carrello
      this.calculateCartTotal();
    }, error => {
      console.error('Errore durante l\'eliminazione dal carrello:', error);
    });
  }

  purchase() {
    this.cartService.purchase().subscribe(
      () => {
        this.router.navigate(['/userBook']); // Reindirizza direttamente alla pagina di gestione dei libri acquistati
      },
      (error) => {
        console.error('Errore durante l\'acquisto:', error);
        alert('Si è verificato un errore durante l\'acquisto. Riprova.');
      }
    );
  }


}
