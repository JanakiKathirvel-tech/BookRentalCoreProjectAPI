using BookRentalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.EFCore
{
    public class BookRentalDBContext : DbContext
    {
        public BookRentalDBContext(DbContextOptions<BookRentalDBContext> options) : base(options) { }


        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(new List<Genre>()
            {
                new Genre
                {
                    GenreId = 1,
                    GenreName = "Classics"
                },
                new Genre
                {
                    GenreId = 2,
                    GenreName = "Dystopian"
                },
                new Genre
                {
                    GenreId = 3,
                    GenreName = "Romance"
                },
                new Genre
                {
                    GenreId = 4,
                    GenreName = "Fantasy"
                },
                new Genre
                {
                    GenreId = 5,
                    GenreName = "Science Fiction"
                },
                new Genre
                {
                    GenreId = 6,
                    GenreName = "Historical Fiction"
                }

            });


            // for default user data
            modelBuilder.Entity<User>().HasData(new List<User>()
            {
                new User {
                    UserId = 1,
                    Name = "JOHN",
                    UserEmail ="John@gmail.com"
                },
                new User {
                    UserId = 2,
                    Name = "Mike",
                    UserEmail ="Janaki.kv2@gmail.com"
                },
                new User {
                    UserId = 3,
                    Name = "LASSE",
                    UserEmail ="Lasse@gmail.com"
                },
                new User {
                    UserId = 4,
                    Name = "EMIL",
                    UserEmail ="Email@gmail.com"
                },
                new User {
                    UserId = 5,
                    Name = "KARTSON",
                    UserEmail ="Karston@gmail.com"
                }
            });

            // for default Books data
            modelBuilder.Entity<Book>().HasData(new List<Book>()
            {
                new Book
                {
                    BookId = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISDN = "9780743273565",
                    GenreId = 1,
                    IsAvailable = true,
                },
                new Book
                {
                    BookId = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    ISDN = "9780060935467",
                    GenreId = 1,
                    IsAvailable= true,
                },
                new Book
                {
                    BookId = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    ISDN = "9780451524935",
                    GenreId = 2,
                    IsAvailable=true
                },
                new Book
                {
                    BookId = 4,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    ISDN = "9780141199078",
                    GenreId = 3,
                    IsAvailable=true,
                },
                new Book
                {
                    BookId = 5,
                    Title = "The Catcher in the Rye",
                    Author = "J.D. Salinger",
                    ISDN = "9780316769488",
                    GenreId = 1,
                    IsAvailable=true
                },
                new Book
                {
                    BookId = 6,
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    ISDN = "9780547928227",
                    GenreId = 4,
                    IsAvailable=true
                },

                new Book
                {
                    BookId = 7,
                    Title = "Fahrenheit 451",
                    Author = "Ray Bradbury",
                    ISDN = "9781451673319",
                    GenreId = 5,
                    IsAvailable=true
                },
                new Book
                {
                    BookId = 8,
                    Title = "The Book Thief",
                    Author = "Markus Zusak",
                    ISDN = "9780375842207",
                    GenreId = 6,
                    IsAvailable =true
                },
                new Book
                {
                    BookId = 9,
                    Title = "Moby-Dick",
                    Author = "Herman Melville",
                    ISDN = "9781503280786",
                    GenreId = 1,
                    IsAvailable=true
                },
                new Book
                {
                    BookId = 10,
                    Title = "War and Peace",
                    Author = "Leo Tolstoy",
                    ISDN = "9781400079988",
                    GenreId = 6,
                    IsAvailable =true
                },


            });
        }

       
    }
}

