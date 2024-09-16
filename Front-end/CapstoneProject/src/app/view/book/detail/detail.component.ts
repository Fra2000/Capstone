import { AuthService } from './../../../services/Account/auth.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../services/Book/book.service';
import { Book } from '../../../interfaces/Book';

@Component({
  selector: 'detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  book: Book | undefined;

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    public authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    const bookId = this.route.snapshot.paramMap.get('id');
    if (bookId) {
      this.bookService.getBookById(+bookId).subscribe({
        next: (book) => {
          this.book = book;

          // Calcola il fullName dell'autore
          if (this.book?.author?.firstName && this.book?.author.lastName) {
            this.book.author.fullName = `${book.author?.firstName} ${book.author?.lastName}`;
          }
        },
        error: (error) => console.error('Error fetching book:', error)
      });
    } else {
      console.error('Book ID is missing in the route parameters.');
    }
  }



  getCoverImagePath(relativePath: string): string {
    return this.bookService.getCoverImagePath(relativePath);
  }

  handleImageError(event: any) {
    this.bookService.handleImageError(event);
  }

  updateBook() {
    if (this.book && this.book.bookId) {
      this.bookService.updateBook(this.book.bookId, this.book).subscribe({
        next: (updatedBook) => {
          console.log('Book updated successfully', updatedBook);
          // Opzionale: Reindirizza o mostra un messaggio di successo
        },
        error: (error) => console.error('Error updating book:', error)
      });
    }
  }


  deleteBook() {
    if (this.authService.hasRole(['Admin', 'SuperAdmin']) && this.book?.bookId) {
      this.bookService.deleteBook(this.book.bookId).subscribe({
        next: () => {
          console.log('Book deleted successfully');
          this.router.navigate(['/']);
          // Opzionale: Reindirizza o aggiorna la vista
        },
        error: (error) => console.error('Error deleting book:', error)
      });
    }
  }

}
