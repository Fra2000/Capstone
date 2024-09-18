import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { BookService } from '../../services/Book/book.service';
import { Cart } from '../../interfaces/cart';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cart: Cart | null = null;

  constructor(private cartService: CartService, private bookService: BookService) { }

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
    if (item.deleteQuantity > 0) {
      this.cartService.removeCartItem(item.userBookId, item.deleteQuantity).subscribe(() => {
        // Aggiorna la quantità o rimuovi completamente l'articolo dal carrello
        if (item.deleteQuantity >= item.quantity) {
          this.cart!.cartItems = this.cart!.cartItems.filter(cartItem => cartItem.userBookId !== item.userBookId);
        } else {
          item.quantity -= item.deleteQuantity;
          this.updateTotal(item); // Aggiorna il totale per l'articolo
        }
        this.calculateCartTotal(); // Aggiorna il totale del carrello
      }, error => {
        console.error('Errore durante l\'eliminazione dal carrello:', error);
      });
    }
  }
}
