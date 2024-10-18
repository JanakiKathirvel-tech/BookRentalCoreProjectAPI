using BookRental.BusinessLayer.Interfaces;
using BookRental.EFCore;
using BookRental.EFCore.DTO;
using BookRentalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.BusinessLayer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookRentalDBContext _context;

        public BookRepository(BookRentalDBContext appDbContext)
        {
            this._context = appDbContext;
        }

        public async Task<IEnumerable<BookDTO>> Search(string name, string genre)
        {
            IQueryable<Book> query = _context.Books;

            if (!string.IsNullOrEmpty(name)  && !string.IsNullOrEmpty(genre))
            {
                query = query.Where(e => e.Title.Contains(name) || e.Genre.GenreName.Contains(genre));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Title.Contains(name)  || e.Genre.GenreName.Contains(name));                           
            }


            var books = from b in query
                        select new BookDTO()
                        {                           
                            Title = b.Title,
                            Author = b.Author,
                            ISDN = b.ISDN,
                            Genre =  b.Genre.GenreName
                        };

            return await books.ToListAsync();            
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            IQueryable<Book> query = _context.Books;

            return await query.ToListAsync();
        }

        public async Task<Book> GetBookbyId(int bookId)
        {
            var book = _context.Books.FindAsync(bookId);
            if (book != null)
            {
                return await book;
            }
            return null;
        }


        private async Task<Book> GetMostOverdueBookAsync()
        {
            return await _context.Rentals
                .Where(r => r.ReturnDate == null &&  r.RentalDate.AddDays(14)  < DateTime.Now)
                .GroupBy(r => r.Book)
                .Select(g => new
                {
                    Book = g.Key,
                    OverdueDays = g.Sum(r => EF.Functions.DateDiffDay(r.RentalDate, DateTime.Now))
                })
                .OrderByDescending(x => x.OverdueDays)
                .Select(x => x.Book)
                .FirstOrDefaultAsync();
        }


        private async Task<Book> GetMostPopularBookAsync()
        {
            return await _context.Rentals
                .GroupBy(r => r.Book)
                .Select(g => new
                {
                    Book = g.Key,
                    RentalCount = g.Count()
                })
                .OrderByDescending(x => x.RentalCount)
                .Select(x => x.Book)
                .FirstOrDefaultAsync();
        }

        private async Task<Book> GetLeastPopularBookAsync()
        {
            return await _context.Rentals
                .GroupBy(r => r.Book)
                .Select(g => new
                {
                    Book = g.Key,
                    RentalCount = g.Count()
                })
                .OrderBy(x => x.RentalCount)
                .Select(x => x.Book)
                .FirstOrDefaultAsync();
        }

        public async Task<BookStatisticsDto> GetBookStatisticsAsync()
        {
            var mostOverdueBook = await GetMostOverdueBookAsync();
            var mostPopularBook = await GetMostPopularBookAsync();
            var leastPopularBook = await GetLeastPopularBookAsync();

            return new BookStatisticsDto
            {
                MostOverdueBook = mostOverdueBook,
                MostPopularBook = mostPopularBook,
                LeastPopularBook = leastPopularBook
            };
        }


       
    }
}
