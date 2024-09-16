import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../../../services/Author/author.service';
import { AuthorRead } from '../../../interfaces/Author';

@Component({
  selector: 'app-update-author',
  templateUrl: './update-author.component.html',
  styleUrls: ['./update-author.component.scss']
})
export class UpdateAuthorComponent implements OnInit {
  author: AuthorRead = {
    authorId: 0,
    firstName: '',
    lastName: '',
    dateOfBirth: new Date(),
    bio: '',
    imagePath: '',
    books: []
  };

  // Variabile separata per gestire il formato dell'input di tipo 'date'
  formattedDateOfBirth: string = '';

  selectedFile: File | null = null;

  constructor(
    public authorService: AuthorService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const authorId = Number(this.route.snapshot.paramMap.get('id'));
    this.authorService.getAuthorById(authorId).subscribe(
      (response: AuthorRead) => {
        this.author = response;

        // Converti la data di nascita in formato yyyy-MM-dd solo per il binding dell'input
        if (this.author.dateOfBirth) {
          const date = new Date(this.author.dateOfBirth);
          this.formattedDateOfBirth = date.toISOString().split('T')[0]; // yyyy-MM-dd
        }
      },
      (error) => {
        console.error('Errore nel caricamento dell\'autore:', error);
      }
    );
  }

  // Metodo per selezionare il file immagine
  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  // Metodo per aggiornare i dati dell'autore
  updateAuthor(): void {
    // Convertire la stringa della data di nascita in un oggetto Date
    if (this.formattedDateOfBirth) {
      this.author.dateOfBirth = new Date(this.formattedDateOfBirth);
    }

    const formData = new FormData();
    formData.append('firstName', this.author.firstName);
    formData.append('lastName', this.author.lastName);
    formData.append('dateOfBirth', this.formattedDateOfBirth); // Usa la data formattata per l'invio
    formData.append('bio', this.author.bio);

    if (this.selectedFile) {
      formData.append('imageFile', this.selectedFile); // Aggiungi il file immagine se presente
    }

    this.authorService.updateAuthor(this.author.authorId, formData).subscribe(
      () => {
        console.log('Autore aggiornato con successo');
        this.router.navigate(['/allAuthors']);
      },
      (error) => {
        console.error('Errore nell\'aggiornamento dell\'autore:', error);
      }
    );
  }
}
