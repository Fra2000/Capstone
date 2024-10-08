import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../services/book.service';
import { GenreService } from '../../../services/genre.service';
import { AuthorService } from '../../../services/author.service';
import { Book } from '../../../interfaces/Book';
import { Genre } from '../../../interfaces/Genre';
import { AuthorRead } from '../../../interfaces/Author';

@Component({
  selector: 'app-update-book',
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.scss']
})
export class UpdateBookComponent implements OnInit {
  book: Partial<Book> = {
    publicationDate: new Date()
  };
  formattedPublicationDate: string = '';
  genres: Genre[] = [];
  authors: AuthorRead[] = [];
  selectedGenres: number[] = [];
  selectedFile: File | null = null;
  authorId: number | null = null;


  constructor(
    private bookService: BookService,
    private genreService: GenreService,
    private authorService: AuthorService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    const bookId = +this.route.snapshot.paramMap.get('id')!;
    this.bookService.getBookById(bookId).subscribe({
      next: (book) => {
        this.book = book;
        this.authorId = this.book.author?.authorId || null;
        this.selectedGenres = this.book.genres?.map(g => g.genreId) || [];
        if (this.book.publicationDate) {
          const date = new Date(this.book.publicationDate);
          const userTimezoneOffset = date.getTimezoneOffset() * 60000;
          const correctedDate = new Date(date.getTime() - userTimezoneOffset);
          this.formattedPublicationDate = correctedDate.toISOString().split('T')[0];
        }
      },
      error: (error) => console.error('Error fetching book:', error)
    });

    this.genreService.getAllGenres().subscribe({
      next: (genres) => this.genres = genres,
      error: (error) => console.error('Error fetching genres:', error)
    });

    this.authorService.getAllAuthors().subscribe({
      next: (authors) => this.authors = authors,
      error: (error) => console.error('Error fetching authors:', error)
    });
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onGenreChange(event: any, genreId: number) {
    if (event.target.checked) {
      this.selectedGenres.push(genreId);
    } else {
      const index = this.selectedGenres.indexOf(genreId);
      if (index > -1) {
        this.selectedGenres.splice(index, 1);
      }
    }
  }

  isGenreSelected(genreId: number): boolean {
    return this.selectedGenres.includes(genreId);
  }

  isAuthorSelected(authorId: number): boolean {
    return this.book.author?.authorId === authorId;
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('name', this.book.name || '');
    formData.append('authorId', this.authorId?.toString() || '');

    formData.append('publicationDate', this.formattedPublicationDate);

    const formattedPrice = this.book.price ? this.book.price.toString().replace(',', '.') : '';
    formData.append('price', formattedPrice);
    formData.append('availableQuantity', this.book.availableQuantity?.toString() || '');
    formData.append('numberOfPages', this.book.numberOfPages?.toString() || '');
    formData.append('description', this.book.description || '');

    this.selectedGenres.forEach((genreId) => {
      formData.append('genreIds', genreId.toString());
    });

    if (this.selectedFile) {
      formData.append('coverImage', this.selectedFile);
    }

    this.bookService.moreUpdateBook(this.book.bookId!, formData).subscribe({
      next: () => {
        console.log('Book updated successfully');
        this.router.navigate(['/']);
      },
      error: (error) => console.error('Error updating book:', error)
    });
  }

  getCoverImagePath(relativePath: string): string {
    return this.bookService.getCoverImagePath(relativePath);
  }

}
