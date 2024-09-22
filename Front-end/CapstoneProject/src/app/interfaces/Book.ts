import { AuthorRead } from "./Author";
import { Genre } from "./Genre";

export interface Book {
  bookId: number;
  name: string;
  numberOfPages: number;
  description: string;
  author?: AuthorRead;
  coverImagePath: string;
  publicationDate: Date;
  price: string;
  availableQuantity: number;
  genres?: Genre[];
}
