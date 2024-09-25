import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GenreService } from '../../../services/genre.service';
import { Genre } from '../../../interfaces/Genre';

@Component({
  selector: 'app-update-genre',
  templateUrl: './update-genre.component.html',
  styleUrls: ['./update-genre.component.scss']
})
export class UpdateGenreComponent implements OnInit {
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
      (response: Genre) => {
        this.genreName = response.genreName;
      },
      (error) => {
        console.error('Errore nel caricamento del genere:', error);
      }
    );
  }

  updateGenre(): void {
    const genreData = { GenreName: this.genreName };
    this.genreService.updateGenre(this.genreId, genreData).subscribe(
      (response) => {
        console.log('Genere aggiornato con successo:', response);
        this.router.navigate(['/allGenre']);
      },
      (error) => {
        console.error('Errore nell\'aggiornamento del genere:', error);
      }
    );
  }
}
