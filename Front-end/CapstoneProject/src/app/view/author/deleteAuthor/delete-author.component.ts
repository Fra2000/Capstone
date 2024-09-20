import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorRead } from '../../../interfaces/Author'; // Assicurati che AuthorRead sia importato correttamente
import { AuthorService } from '../../../services/Author/author.service';

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
    // Ottieni l'ID dell'autore dalla route
    this.authorId = Number(this.route.snapshot.paramMap.get('id'));

    // Carica i dettagli dell'autore tramite l'ID
    this.authorService.getAuthorById(this.authorId).subscribe(
      (data: AuthorRead) => {
        this.author = data; // Assegna i dettagli dell'autore all'oggetto author
      },
      (error) => {
        console.error('Errore nel recupero dell\'autore:', error);
      }
    );
  }

  // Metodo per confermare l'eliminazione
  confirmDelete(): void {
    this.authorService.deleteAuthor(this.authorId).subscribe(
      () => {
        console.log('Autore eliminato con successo');
        this.router.navigate(['/allAuthors']); // Reindirizza alla lista degli autori
      },
      (error) => {
        console.error('Errore nell\'eliminazione dell\'autore:', error);
      }
    );
  }

  // Metodo per annullare l'operazione e tornare indietro
  cancel(): void {
    this.router.navigate(['/allAuthors']);
  }
}
