using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO;
using CapstoneBack.Models.DTO.ReviewDTO;

namespace CapstoneBack.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReviewReadDto> AddReviewAsync(int userId, ReviewCreateDto reviewDto)
        {
            // Verifica se l'utente ha già lasciato una recensione per questo libro.
            var existingReview = await _context.UserReviews
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.BookId == reviewDto.BookId);

            if (existingReview != null)
            {
                throw new InvalidOperationException("Review already exists. Each user can only create one review per book.");
            }

            // Verifica se l'utente ha effettivamente "acquistato" il libro.
            var userBookStatus = await _context.UserBookStatuses
                .FirstOrDefaultAsync(ubs => ubs.UserId == userId && ubs.BookId == reviewDto.BookId);

            if (userBookStatus == null)
            {
                throw new InvalidOperationException("Review not allowed. Book not purchased by the user.");
            }

            var review = new UserReview
            {
                UserId = userId,
                BookId = reviewDto.BookId,
                Rating = reviewDto.Rating,
                ReviewText = reviewDto.ReviewText,
                ReviewDate = DateTime.UtcNow
            };

            _context.UserReviews.Add(review);
            await _context.SaveChangesAsync();

            return new ReviewReadDto
            {
                UserReviewId = review.UserReviewId,
                BookId = review.BookId,
                UserName = _context.Users.Where(u => u.UserId == userId).Select(u => u.UserName).FirstOrDefault(),
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                ReviewDate = review.ReviewDate
            };
        }



        public async Task<IEnumerable<ReviewReadDto>> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.UserReviews
                .Where(r => r.BookId == bookId)
                .Include(r => r.User)
                .Select(r => new ReviewReadDto
                {
                    UserReviewId = r.UserReviewId,
                    BookId = r.BookId,
                    UserName = r.User.UserName,
                    Rating = r.Rating,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewReadDto>> GetReviewsByUserIdAsync(int userId)
        {
            return await _context.UserReviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Book)
                .Select(r => new ReviewReadDto
                {
                    UserReviewId = r.UserReviewId,
                    BookId = r.Book.BookId,
                    UserName = r.User.UserName,
                    Rating = r.Rating,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate
                })
                .ToListAsync();
        }

        public async Task<ReviewReadDto> GetReviewByIdAsync(int reviewId)
        {
            var review = await _context.UserReviews
                .Where(r => r.UserReviewId == reviewId)
                .Include(r => r.User)
                .Select(r => new ReviewReadDto
                {
                    UserReviewId = r.UserReviewId,
                    BookId = r.BookId,
                    UserName = r.User.UserName,
                    Rating = r.Rating,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate
                })
                .FirstOrDefaultAsync();

            return review;
        }

        public async Task<ReviewReadDto> UpdateReviewAsync(int userId, int reviewId, ReviewCreateDto reviewDto)
        {
            var review = await _context.UserReviews
                .FirstOrDefaultAsync(ur => ur.UserReviewId == reviewId && ur.UserId == userId);

            if (review == null)
            {
                throw new InvalidOperationException("Review not found. Cannot update a non-existing review.");
            }

            // Aggiornamento dei campi della recensione
            review.Rating = reviewDto.Rating;
            review.ReviewText = reviewDto.ReviewText;
            review.ReviewDate = DateTime.UtcNow;  // Aggiorna la data alla modifica attuale

            _context.UserReviews.Update(review);
            await _context.SaveChangesAsync();

            return new ReviewReadDto
            {
                UserReviewId = review.UserReviewId,
                BookId = review.BookId,
                UserName = _context.Users.Where(u => u.UserId == userId).Select(u => u.UserName).FirstOrDefault(),
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                ReviewDate = review.ReviewDate
            };
        }



        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.UserReviews.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            _context.UserReviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
