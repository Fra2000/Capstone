export interface AuthorRead {
  authorId: number;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  bio: string;
  imagePath: string;
  books: string[];
}
