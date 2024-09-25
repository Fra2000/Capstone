import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorService } from '../../../services/author.service';
import { AuthorRead } from '../../../interfaces/Author';

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


  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }


  createAuthor(): void {
    const formData = new FormData();
    formData.append('firstName', this.author.firstName);
    formData.append('lastName', this.author.lastName);
    formData.append('dateOfBirth', this.author.dateOfBirth);
    formData.append('bio', this.author.bio);

    if (this.selectedFile) {
      formData.append('imageFile', this.selectedFile);
    }


    this.authorService.createAuthor(formData).subscribe(
      (response: AuthorRead) => {
        console.log('Autore creato con successo:', response);
        this.router.navigate(['/allAuthors']);
      },
      (error) => {
        console.error('Errore nella creazione dell\'autore:', error);
      }
    );
  }
}
