import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GenreService } from '../../../services/Genre/genre.service';
import { Book } from '../../../interfaces/Book';

@Component({
  selector: 'app-detail-genre',
  templateUrl: './detail-genre.component.html',
  styleUrls: ['./detail-genre.component.scss']
})
export class DetailGenreComponent implements OnInit {
  genre: any; // Salviamo l'intero oggetto 'genre'

  constructor(
    private route: ActivatedRoute,
    private genreService: GenreService
  ) { }

  ngOnInit(): void {
    const genreId = this.route.snapshot.paramMap.get('id');
    console.log('Genre ID:', genreId);
    if (genreId) {
      this.genreService.getBooksByGenreId(+genreId).subscribe({
        next: (genre) => {
          console.log('Books fetched:', genre);
          this.genre = genre; // Popola i dati del genere con i libri associati
        },
        error: (error) => console.error('Error fetching books by genre:', error)
      });
    }
  }

}
