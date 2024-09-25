import { Component } from '@angular/core';
import { GenreService } from '../../../services/genre.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-genre',
  templateUrl: './create-genre.component.html',
  styleUrls: ['./create-genre.component.scss']
})
export class CreateGenreComponent {
  genreName: string = '';

  constructor(private genreService: GenreService, private router: Router) { }

  createGenre(): void {
    const genreData = { GenreName: this.genreName };
    this.genreService.createGenre(genreData).subscribe(
      (response) => {
        console.log('Genere creato con successo:', response);
        this.router.navigate(['/allGenre']);  // Reindirizzamento alla lista di tutti i generi
      },
      (error) => {
        console.error('Errore nella creazione del genere:', error);
      }
    );
  }
}
