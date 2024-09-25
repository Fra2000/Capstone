import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorRead } from '../../../interfaces/Author';
import { AuthorService } from '../../../services/author.service';

@Component({
  selector: 'app-delete-author',
  templateUrl: './delete-author.component.html',
  styleUrls: ['./delete-author.component.scss']
})
export class DeleteAuthorComponent implements OnInit {
  authorId!: number;
  author!: AuthorRead;

  constructor(
    private authorService: AuthorService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.authorId = Number(this.route.snapshot.paramMap.get('id'));


    this.authorService.getAuthorById(this.authorId).subscribe(
      (data: AuthorRead) => {
        this.author = data;
      },
      (error) => {
        console.error('Errore nel recupero dell\'autore:', error);
      }
    );
  }


  confirmDelete(): void {
    this.authorService.deleteAuthor(this.authorId).subscribe(
      () => {
        console.log('Autore eliminato con successo');
        this.router.navigate(['/allAuthors']);
      },
      (error) => {
        console.error('Errore nell\'eliminazione dell\'autore:', error);
      }
    );
  }


  cancel(): void {
    this.router.navigate(['/allAuthors']);
  }
}
