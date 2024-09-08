export interface Book {
  bookId: number;
  name: string;
  authorName: string;
  coverImagePath: string;
  publicationDate: Date;
  price: number;
  genres: string[];
}
