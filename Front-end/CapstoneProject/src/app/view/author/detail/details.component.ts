// detail-author.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../../../services/Author/author.service';
import { AuthorRead } from '../../../interfaces/Author';
import { Book } from '../../../interfaces/Book'
import { BookService } from '../../../services/Book/book.service';

@Component({
  selector: 'app-detail-author',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailAuthorComponent implements OnInit {
  author: AuthorRead | undefined;
  books: Book[] = [];

  constructor(
    private route: ActivatedRoute,
    private authorService: AuthorService,
    private router: Router,
    private bookService: BookService
  ) { }

  ngOnInit() {
    const authorId = this.route.snapshot.paramMap.get('id');
    if (authorId) {
      this.authorService.getAuthorById(+authorId).subscribe({
        next: (author) => {
          this.author = author;
          this.loadBooksForAuthor(author.authorId);
        },
        error: (error) => console.error('Error fetching author:', error)
      });
    } else {
      console.error('Author ID is missing in the route parameters.');
    }
  }

  loadBooksForAuthor(authorId: number) {
    // Aggiungi un metodo nel tuo BookService se non esiste già per caricare libri per autore
    this.bookService.getBooksByAuthorId(authorId).subscribe({
      next: (books) => this.books = books,
      error: (error) => console.error(`Error fetching books for author ${authorId}:`, error)
    });
  }

  getAuthorImagePath(relativePath: string): string {
    return this.authorService.getAuthorImagePath(relativePath);
  }

  handleImageError(event: any) {
    this.authorService.handleImageError(event);
  }
}
