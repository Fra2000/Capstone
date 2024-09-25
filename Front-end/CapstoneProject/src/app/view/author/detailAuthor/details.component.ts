import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../../../services/author.service';
import { AuthorRead } from '../../../interfaces/Author';
import { Book } from '../../../interfaces/Book'
import { BookService } from '../../../services/book.service';

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

    this.bookService.getBooksByAuthorId(authorId).subscribe({
      next: (books) => this.books = books,
      error: (error) => console.error(`Error fetching books for author ${authorId}:`, error)
    });
  }

  getAuthorImagePath(relativePath: string): string {
    return this.authorService.getAuthorImagePath(relativePath);
  }

  getCoverImagePath(relativePath: string): string {
    if (!relativePath) {
      return 'https://localhost:7097/images/Book/default.png';
    }
    return `https://localhost:7097/${relativePath}`;
  }


  handleImageError(event: any) {
    event.target.src = 'https://localhost:7097/images/Book/default.png';
  }

}
