import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../../../services/author.service';
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


        if (this.author.dateOfBirth) {
          const date = new Date(this.author.dateOfBirth);
          const userTimezoneOffset = date.getTimezoneOffset() * 60000;
          const correctedDate = new Date(date.getTime() - userTimezoneOffset);
          this.formattedDateOfBirth = correctedDate.toISOString().split('T')[0];
        }
      },
      (error) => {
        console.error('Errore nel caricamento dell\'autore:', error);
      }
    );
  }



  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }


  updateAuthor(): void {

    if (this.formattedDateOfBirth) {
      this.author.dateOfBirth = new Date(this.formattedDateOfBirth);
    }

    const formData = new FormData();
    formData.append('firstName', this.author.firstName);
    formData.append('lastName', this.author.lastName);
    formData.append('dateOfBirth', this.formattedDateOfBirth);
    formData.append('bio', this.author.bio);

    if (this.selectedFile) {
      formData.append('imageFile', this.selectedFile);
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
