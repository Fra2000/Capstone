import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookService } from '../../../services/Book/book.service';
import { GenreService } from '../../../services/Genre/genre.service';
import { AuthorService } from '../../../services/Author/author.service';
import { Book } from '../../../interfaces/Book';
import { Genre } from '../../../interfaces/Genre';
import { AuthorRead } from '../../../interfaces/Author';

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrls: ['./create-book.component.scss']
})
export class CreateBookComponent implements OnInit {
  book: Partial<Book> = {};
  genres: Genre[] = [];
  authors: AuthorRead[] = [];
  selectedGenres: number[] = [];
  selectedFile: File | null = null;
  authorId: number | null = null;

  constructor(
    private bookService: BookService,
    private genreService: GenreService,
    private authorService: AuthorService,
    private router: Router
  ) { }

  ngOnInit() {


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

  onSubmit() {
    const formData = new FormData();
    formData.append('name', this.book.name || '');
    formData.append('authorId', this.authorId?.toString() || '');

    // Assicurati che la data di pubblicazione sia formattata correttamente come yyyy-MM-dd
    const publicationDateString = this.book.publicationDate
      ? (this.book.publicationDate instanceof Date
        ? this.book.publicationDate.toISOString().split('T')[0]
        : this.book.publicationDate)
      : '';
    formData.append('publicationDate', publicationDateString);

    formData.append('price', this.book.price?.toString() || '');
    formData.append('availableQuantity', this.book.availableQuantity?.toString() || '');
    formData.append('numberOfPages', this.book.numberOfPages?.toString() || '');
    formData.append('description', this.book.description || '');

    // Aggiungi i generi selezionati come una lista
    this.selectedGenres.forEach((genreId) => {
      formData.append('genreIds', genreId.toString());
    });

    // Aggiungi l'immagine se selezionata
    if (this.selectedFile) {
      formData.append('coverImage', this.selectedFile);
    }

    this.bookService.createBook(formData).subscribe({
      next: () => {
        console.log('Book created successfully');
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Error creating book:', error);
      }
    });
  }



  onGenreChange(event: any, genreId: number) {
    if (event.target.checked) {
      // Se la checkbox è selezionata, aggiungi il genere all'array
      this.selectedGenres.push(genreId);
    } else {
      // Se la checkbox è deselezionata, rimuovi il genere dall'array
      const index = this.selectedGenres.indexOf(genreId);
      if (index > -1) {
        this.selectedGenres.splice(index, 1);
      }
    }
  }

}
