﻿// <auto-generated />
using System;
using CapstoneBack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CapstoneBack.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CapstoneBack.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("CapstoneBack.Models.BookGenre", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("BookGenreId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("BookGenres");
                });

            modelBuilder.Entity("CapstoneBack.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("CapstoneBack.Models.LoyaltyCardType", b =>
                {
                    b.Property<int>("LoyaltyCardTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoyaltyCardTypeId"));

                    b.Property<string>("AdditionalBenefits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("MinimumSpend")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("LoyaltyCardTypeId");

                    b.ToTable("LoyaltyCardTypes");
                });

            modelBuilder.Entity("CapstoneBack.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CapstoneBack.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("CapstoneBack.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CapstoneBack.Models.UserBook", b =>
                {
                    b.Property<int>("UserBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserBookId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserBookId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBooks");
                });

            modelBuilder.Entity("CapstoneBack.Models.UserBookStatus", b =>
                {
                    b.Property<int>("UserBookStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserBookStatusId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int?>("CurrentPage")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserBookStatusId");

                    b.HasIndex("BookId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBookStatuses");
                });

            modelBuilder.Entity("CapstoneBack.Models.UserLoyaltyCard", b =>
                {
                    b.Property<int>("UserLoyaltyCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserLoyaltyCardId"));

                    b.Property<DateTime>("CardAssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoyaltyCardTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalSpent")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserLoyaltyCardId");

                    b.HasIndex("LoyaltyCardTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLoyaltyCards");
                });

            modelBuilder.Entity("CapstoneBack.Models.BookGenre", b =>
                {
                    b.HasOne("CapstoneBack.Models.Book", "Book")
                        .WithMany("BookGenres")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBack.Models.Genre", "Genre")
                        .WithMany("BookGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("CapstoneBack.Models.User", b =>
                {
                    b.HasOne("CapstoneBack.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CapstoneBack.Models.UserBook", b =>
                {
                    b.HasOne("CapstoneBack.Models.Book", "Book")
                        .WithMany("UserBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBack.Models.User", "User")
                        .WithMany("UserBooks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CapstoneBack.Models.UserBookStatus", b =>
                {
                    b.HasOne("CapstoneBack.Models.Book", "Book")
                        .WithMany("UserBookStatuses")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBack.Models.Status", "Status")
                        .WithMany("UserBookStatuses")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBack.Models.User", "User")
                        .WithMany("UserBookStatuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CapstoneBack.Models.UserLoyaltyCard", b =>
                {
                    b.HasOne("CapstoneBack.Models.LoyaltyCardType", "LoyaltyCardType")
                        .WithMany("UserLoyaltyCards")
                        .HasForeignKey("LoyaltyCardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBack.Models.User", "User")
                        .WithMany("UserLoyaltyCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoyaltyCardType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CapstoneBack.Models.Book", b =>
                {
                    b.Navigation("BookGenres");

                    b.Navigation("UserBookStatuses");

                    b.Navigation("UserBooks");
                });

            modelBuilder.Entity("CapstoneBack.Models.Genre", b =>
                {
                    b.Navigation("BookGenres");
                });

            modelBuilder.Entity("CapstoneBack.Models.LoyaltyCardType", b =>
                {
                    b.Navigation("UserLoyaltyCards");
                });

            modelBuilder.Entity("CapstoneBack.Models.Status", b =>
                {
                    b.Navigation("UserBookStatuses");
                });

            modelBuilder.Entity("CapstoneBack.Models.User", b =>
                {
                    b.Navigation("UserBookStatuses");

                    b.Navigation("UserBooks");

                    b.Navigation("UserLoyaltyCards");
                });
#pragma warning restore 612, 618
        }
    }
}
