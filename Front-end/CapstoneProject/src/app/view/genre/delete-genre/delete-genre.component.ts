import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GenreService } from '../../../services/genre.service';

@Component({
  selector: 'app-delete-genre',
  templateUrl: './delete-genre.component.html',
  styleUrls: ['./delete-genre.component.scss']
})
export class DeleteGenreComponent implements OnInit {
  genreId: number = 0;
  genreName: string = '';

  constructor(
    private genreService: GenreService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.genreId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadGenre();
  }

  loadGenre(): void {
    this.genreService.getGenreById(this.genreId).subscribe(
      (response) => {
        this.genreName = response.genreName;
      },
      (error) => {
        console.error('Errore nel caricamento del genere:', error);
      }
    );
  }

  deleteGenre(): void {
    this.genreService.deleteGenre(this.genreId).subscribe(
      () => {
        console.log('Genere eliminato con successo');
        this.router.navigate(['/allGenre']);
      },
      (error) => {
        console.error('Errore nell\'eliminazione del genere:', error);
      }
    );
  }
}
