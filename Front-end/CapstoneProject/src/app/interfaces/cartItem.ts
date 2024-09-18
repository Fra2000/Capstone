export interface CartItem {
  userBookId: number;
  bookId: number;
  bookName: string;
  coverImagePath: string;
  quantity: number;
  pricePerUnit: number;
  totalPrice: number;
  authorName: string;
}
