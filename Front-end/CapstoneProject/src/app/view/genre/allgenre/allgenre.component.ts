import { Component, OnInit } from '@angular/core';
import { GenreService } from '../../../services/genre.service';
import { Genre } from '../../../interfaces/Genre';

@Component({
  selector: 'app-all-genre',
  templateUrl: './allgenre.component.html',
  styleUrls: ['./allgenre.component.scss']
})
export class AllGenreComponent implements OnInit {
  genres: Genre[] = [];

  constructor(private genreService: GenreService) { }

  ngOnInit(): void {
    this.genreService.getAllGenres().subscribe({
      next: (genres) => this.genres = genres,
      error: (error) => console.error('Error fetching genres:', error)
    });
  }
}
