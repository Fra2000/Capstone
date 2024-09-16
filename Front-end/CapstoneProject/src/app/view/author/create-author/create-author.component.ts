import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorService } from '../../../services/Author/author.service';
import { AuthorRead } from '../../../interfaces/Author'; // Utilizziamo AuthorRead

@Component({
  selector: 'app-create-author',
  templateUrl: './create-author.component.html',
  styleUrls: ['./create-author.component.scss']
})
export class CreateAuthorComponent {
  author = {
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    bio: ''
  };
  selectedFile: File | null = null;

  constructor(
    private authorService: AuthorService,
    private router: Router
  ) { }

  // Metodo per selezionare il file immagine
  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  // Metodo per creare un autore
  createAuthor(): void {
    const formData = new FormData();
    formData.append('firstName', this.author.firstName);
    formData.append('lastName', this.author.lastName);
    formData.append('dateOfBirth', this.author.dateOfBirth);
    formData.append('bio', this.author.bio);

    if (this.selectedFile) {
      formData.append('imageFile', this.selectedFile);
    }

    // Tipizziamo correttamente la risposta come AuthorRead
    this.authorService.createAuthor(formData).subscribe(
      (response: AuthorRead) => {
        console.log('Autore creato con successo:', response);
        this.router.navigate(['/allAuthors']); // Reindirizza alla lista degli autori
      },
      (error) => {
        console.error('Errore nella creazione dell\'autore:', error);
      }
    );
  }
}
