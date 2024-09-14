export interface BookRead {
  bookId: number;
  name: string;
  numberOfPages: number;
  description: string;
  authorName: string;
  coverImagePath: string;
  publicationDate: Date;
  price: number;
  availableQuantity: number;
  genres: string[];
}
