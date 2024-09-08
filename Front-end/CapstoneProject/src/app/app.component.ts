import { Component, OnInit } from '@angular/core';
import { BookService } from './services/book.service';
import { Book } from './interfaces/Book';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  books: Book[] = [];

  constructor(private bookService: BookService) {}

  ngOnInit() {
    this.bookService.getAllBooks().subscribe({
      next: (books) => this.books = books,
      error: (error) => console.error('Error fetching books:', error)
    });
  }

  getCoverImagePath(relativePath: string): string {
    if (!relativePath) {
      return 'https://localhost:7097/images/Book/default.jpg'; // Fornisci un'immagine di fallback se non presente
    }
    return `https://localhost:7097/${relativePath}`;
  }

  handleImageError(event: any) {
    event.target.src = 'https://localhost:7097/images/Book/default.jpg'; // Percorso dell'immagine di fallback
  }
}
