export interface UserBookStatus {
  bookId: number;
  bookName: string;
  statusName: string;
  currentPage?: number;
  dateUpdated: Date;
  coverImagePath: string;
  totalPages: number;
  purchaseDate: Date;
}

