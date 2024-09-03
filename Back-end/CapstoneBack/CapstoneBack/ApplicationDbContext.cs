using CapstoneBack.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace CapstoneBack
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Definizione delle DbSet per ogni entità
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<UserBookStatus> UserBookStatuses { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<LoyaltyCardType> LoyaltyCardTypes { get; set; }
        public DbSet<UserLoyaltyCard> UserLoyaltyCards { get; set; }

        // Configurazione delle relazioni e delle chiavi composte
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relazione molti-a-molti tra Book e Genre
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.GenreId);

            // Configurazione delle relazioni per UserBook
            modelBuilder.Entity<UserBook>()
                .HasKey(ub => ub.UserBookId);

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBooks)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookId);

            // Relazione per UserBookStatus
            modelBuilder.Entity<UserBookStatus>()
                .HasOne(ubs => ubs.User)
                .WithMany(u => u.UserBookStatuses)
                .HasForeignKey(ubs => ubs.UserId);

            modelBuilder.Entity<UserBookStatus>()
                .HasOne(ubs => ubs.Book)
                .WithMany(b => b.UserBookStatuses)
                .HasForeignKey(ubs => ubs.BookId);

            modelBuilder.Entity<UserBookStatus>()
                .HasOne(ubs => ubs.Status)
                .WithMany(s => s.UserBookStatuses)
                .HasForeignKey(ubs => ubs.StatusId);

            // Configurazione per UserLoyaltyCard
            modelBuilder.Entity<UserLoyaltyCard>()
                .HasOne(ulc => ulc.User)
                .WithMany(u => u.UserLoyaltyCards)
                .HasForeignKey(ulc => ulc.UserId);

            modelBuilder.Entity<UserLoyaltyCard>()
                .HasOne(ulc => ulc.LoyaltyCardType)
                .WithMany(lct => lct.UserLoyaltyCards)
                .HasForeignKey(ulc => ulc.LoyaltyCardTypeId);

            // Configurazione per Author e Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Role>().HasData(
              new Role { RoleId = 1, RoleName = "Admin" },
              new Role { RoleId = 2, RoleName = "User" },
              new Role { RoleId = 3, RoleName = "SuperAdmin" }
            );

            // Creazione dell'utente SuperAdmin
            var superAdminUser = new User
            {
                UserId = 1,
                FirstName = "Super",
                LastName = "Admin",
                UserName = "SuperAdmin",
                Email = "admin@admin.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                RoleId = 3, // Associa il ruolo SuperAdmin
                ImagePath = "images/Account/default.jpg"
            };

            modelBuilder.Entity<User>().HasData(superAdminUser);

        }
    }
}
