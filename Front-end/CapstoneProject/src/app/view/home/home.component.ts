import { Component, OnInit } from '@angular/core';
import { Book } from '../../interfaces/Book';
import { BookService } from '../../services/Book/book.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  books: Book[] = [];

  constructor(private bookService: BookService) {}

  ngOnInit() {
    this.bookService.getAllBooks().subscribe({
      next: (books) => this.books = books,
      error: (error) => console.error('Error fetching books:', error)
    });
  }

  getCoverImagePath(relativePath: string): string {
    return this.bookService.getCoverImagePath(relativePath);
  }

  handleImageError(event: any) {
    this.bookService.handleImageError(event);
  }

}
