import { Book } from "./Book";

export interface AuthorRead {
  authorId: number;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  bio: string;
  imagePath: string;
  books: Book[];
  fullName?: string;
}
