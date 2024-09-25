import { Component, OnInit } from '@angular/core';
import { UserBookService } from '../../services/userBook.service';
import { UserBookStatus } from '../../interfaces/UserBookStatus';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-user-book',
  templateUrl: './user-book.component.html',
  styleUrls: ['./user-book.component.scss'],
  animations: [
    trigger('fadeInOut', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate('300ms ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ]),
      transition(':leave', [
        animate('300ms ease-in', style({ opacity: 0, transform: 'translateY(-10px)' }))
      ])
    ])
  ]
})
export class UserBookComponent implements OnInit {
  userBooks: UserBookStatus[] = [];
  toStartBooks: UserBookStatus[] = [];
  inProgressBooks: UserBookStatus[] = [];
  completedBooks: UserBookStatus[] = [];
  loading: boolean = true;
  updating: boolean = false;
  showSection = {
    toStart: false,
    inProgress: false,
    completed: false
  };

  constructor(private userBookService: UserBookService) { }

  ngOnInit(): void {
    console.log('Componente inizializzato');
    this.loadUserBooks();
  }

  loadUserBooks(): void {
    this.loading = true;
    console.log('Inizio caricamento dei libri dell\'utente...');
    this.userBookService.getUserBookStatuses().subscribe(
      (data) => {
        console.log('Dati ricevuti dal servizio:', data);
        this.userBooks = data;
        this.categorizeBooks();
        this.loading = false;
      },
      (error) => {
        console.error("Errore durante il recupero dei dati dei libri dell'utente:", error);
        this.loading = false;
      }
    );
  }

  categorizeBooks(): void {
    console.log('Inizio categorizzazione dei libri...');
    this.toStartBooks = this.userBooks.filter(book => book.statusName.toLowerCase() === 'da iniziare');
    this.inProgressBooks = this.userBooks.filter(book => book.statusName.toLowerCase() === 'in corso');
    this.completedBooks = this.userBooks.filter(book => book.statusName.toLowerCase() === 'terminati');

    console.log('Libri da iniziare:', this.toStartBooks);
    console.log('Libri in corso:', this.inProgressBooks);
    console.log('Libri terminati:', this.completedBooks);

    this.toStartBooks.forEach(book => {
      if (book.currentPage === null || book.currentPage === undefined) {
        console.log(`Inizializzazione di currentPage a 0 per il libro con ID: ${book.bookId}`);
        book.currentPage = 0;
      }
    });
  }

  getCoverImagePath(relativePath: string): string {
    if (!relativePath) {
      return 'https://localhost:7097/images/Book/default.png';
    }
    const correctedPath = relativePath.replace(/\\/g, '/');
    const fullPath = `https://localhost:7097/${correctedPath}`;
    console.log(`Percorso corretto dell'immagine di copertina per il libro: ${fullPath}`);
    return fullPath;
  }

  handleImageError(event: any) {
    console.warn('Errore nel caricamento dell\'immagine, caricando l\'immagine di default.');
    event.target.src = 'https://localhost:7097/images/Book/default.jpg';
  }

  changeBookStatus(book: UserBookStatus, newStatus: string): void {
    this.updating = true;
    let currentPageToSend: number | undefined = undefined;

    if (newStatus === 'In corso') {

      if (book.currentPage === null || book.currentPage === undefined || book.currentPage < 1) {
        currentPageToSend = 1;
      } else if (book.currentPage > book.totalPages) {
        currentPageToSend = book.totalPages;
      } else {
        currentPageToSend = book.currentPage;
      }
    }


    console.log(`Chiamata al servizio con BookId: ${book.bookId}, NewStatus: ${newStatus}, CurrentPageToSend: ${currentPageToSend}`);

    this.userBookService.updateBookStatus(book.bookId, newStatus, currentPageToSend).subscribe(
      () => {
        console.log(`Stato del libro con ID ${book.bookId} aggiornato con successo a "${newStatus}"`);
        book.statusName = newStatus;
        if (newStatus === 'Terminati') {
          book.currentPage = book.totalPages;
        }
        this.categorizeBooks();
        this.updating = false;
      },
      (error) => {
        console.error('Errore durante l\'aggiornamento dello stato del libro:', error);
        this.updating = false;
        alert('Si è verificato un errore durante l\'aggiornamento. Riprova.');
      }
    );
  }

  updateCurrentPage(book: UserBookStatus): void {
    console.log(`Invio aggiornamento: bookId=${book.bookId}, currentPage=${book.currentPage}`);
    if (book.currentPage !== null && book.currentPage !== undefined && book.currentPage >= 1 && book.currentPage <= book.totalPages) {
      this.userBookService.updateBookStatus(book.bookId, 'In Corso', book.currentPage).subscribe(
        () => {
          console.log(`Pagina corrente aggiornata con successo a ${book.currentPage} per il libro con ID ${book.bookId}`);
        },
        (error) => {
          console.error('Errore durante l\'aggiornamento della pagina corrente del libro:', error);
          alert('Si è verificato un errore durante l\'aggiornamento della pagina corrente. Riprova.');
        }
      );
    }
  }


  trackByBookId(index: number, book: UserBookStatus): number {
    return book.bookId;
  }

  toggleSection(section: 'toStart' | 'inProgress' | 'completed'): void {
    this.showSection[section] = !this.showSection[section];
  }
}
