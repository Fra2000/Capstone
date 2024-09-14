// all-authors.component.ts
import { Component, OnInit } from '@angular/core';
import { AuthorService } from '../../../services/Author/author.service.service';
import { AuthorRead } from '../../../interfaces/AuthorRead'


@Component({
  selector: 'app-all-authors',
  templateUrl: './all.component.html',
  styleUrls: ['./all.component.scss']
})
export class AllAuthorsComponent implements OnInit {
  authors: AuthorRead[] = [];

  constructor(private authorService: AuthorService) { }

  ngOnInit() {
    this.authorService.getAllAuthors().subscribe({
      next: (authors) => {
        this.authors = authors.map(author => {
          console.log(author.imagePath);
          return {
            ...author,
            dateOfBirth: new Date(author.dateOfBirth) // Assicurati che dateOfBirth sia un oggetto Date
          };
        });
      },
      error: (err) => console.error('Error fetching authors:', err)
    });
  }

  getAuthorImagePath(relativePath: string): string {
    return this.authorService.getAuthorImagePath(relativePath);
  }

  handleImageError(event: any) {
    this.authorService.handleImageError(event);
  }
}
