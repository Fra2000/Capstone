import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../../../services/Author/author.service';

@Component({
  selector: 'app-delete-author',
  templateUrl: './delete-author.component.html',
  styleUrls: ['./delete-author.component.scss']
})
export class DeleteAuthorComponent implements OnInit {
  authorId!: number;

  constructor(
    private authorService: AuthorService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    // Ottieni l'ID dell'autore dalla route
    this.authorId = Number(this.route.snapshot.paramMap.get('id'));
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
