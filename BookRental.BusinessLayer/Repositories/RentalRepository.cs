using BookRental.BusinessLayer.Interfaces;
using BookRental.EFCore;
using BookRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.BusinessLayer.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly BookRentalDBContext _context;

        public RentalRepository(BookRentalDBContext appDbContext)
        {
            this._context = appDbContext;
        }      


         public async Task<Rental> RentBook(int bookId, int userId)
        {
            var book = await _context.Books.FindAsync(bookId);
            //if (book == null || !book.IsAvailable)
            //    return BadRequest("Book is not available");
            if (book != null)
            {
                var rental = new Rental
                {
                    BookId = bookId,
                    UserId = userId,
                    RentalDate = DateTime.UtcNow
                };

                _context.Rentals.Add(rental);
                book.IsAvailable = false;
                await _context.SaveChangesAsync();
                return rental;
            }
            return null;

        }


        public async Task<IEnumerable<Rental>> GetAllRentalBooks()
        {
            IQueryable<Rental> query = _context.Rentals;

            return await query.ToListAsync();
        }

        public async Task<Rental> GetRentalbyId(int rentalId)
        {
            var rental = _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                return await rental;
            }
            return null;
        }

        public async Task<Rental> ReturnBook(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.ReturnDate = DateTime.UtcNow;

                var book = await _context.Books.FindAsync(rental.BookId);
                if (book != null)
                    book.IsAvailable = true;

                await _context.SaveChangesAsync();
                return rental;
            }
            return null;
      

        }




      

    }
}
