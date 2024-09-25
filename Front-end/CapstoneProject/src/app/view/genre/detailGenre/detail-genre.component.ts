import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GenreService } from '../../../services/genre.service';

@Component({
  selector: 'app-detail-genre',
  templateUrl: './detail-genre.component.html',
  styleUrls: ['./detail-genre.component.scss']
})
export class DetailGenreComponent implements OnInit {
  genre: any;

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
          this.genre = genre;
        },
        error: (error) => console.error('Error fetching books by genre:', error)
      });
    }
    this.route.paramMap.subscribe(params => {
      const genreIdParam = params.get('id');
      if (genreIdParam) {
        const genreId = +genreIdParam;
        this.loadGenreDetails(genreId);
      }
    });
  }

  getCoverImagePath(relativePath: string): string {
    if (!relativePath) {
      return 'https://localhost:7097/images/Book/default.png';
    }
    return `https://localhost:7097/${relativePath}`;
  }

  handleImageError(event: any) {
    event.target.src = 'https://localhost:7097/images/Book/default.png';
  }

  loadGenreDetails(genreId: number) {
    this.genreService.getBooksByGenreId(genreId).subscribe({
      next: (genre) => {
        this.genre = genre;
      },
      error: (error) => console.error('Error fetching genre details:', error)
    });
  }


}
