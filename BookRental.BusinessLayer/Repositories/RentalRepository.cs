using BookRental.BusinessLayer.Interfaces;
using BookRental.EFCore;
using BookRental.EFCore.DTO;
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
        private readonly EmailService _emailService;

        public RentalRepository(BookRentalDBContext appDbContext, EmailService emailService)
        {
            this._context = appDbContext;
            _emailService = emailService;
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


        public async Task<IEnumerable<RentalDTO>> GetAllRentalBooks()
        {
            IQueryable<Rental> query = _context.Rentals;

            var rentals = from b in query
                          select new RentalDTO()
                          {
                              RentalId = b.RentalId,
                              BookName = b.Book.Title,
                              UserName = b.User.Name,
                              RentalDate = b.RentalDate.ToString("yyyy-MM-dd hh:mm"),
                              ReturnDate = b.ReturnDate != null? b.ReturnDate.ToString() : ""
                        };

            return await rentals.ToListAsync();
        }

        public async Task<Rental> GetRentalbyId(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                return rental;
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

        public async Task MarkOverdueRentalsAsync()
        {
            var overdueRentals = await _context.Rentals
                    .Where(r => !r.IsOverdue && r.ReturnDate == null && EF.Functions.DateDiffDay(r.RentalDate, DateTime.Now) > 14)
                    .ToListAsync();

            foreach (var rental in overdueRentals)
             {
                rental.IsOverdue = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SendMailOverdueNotificationsAsync()
        {
            var overdueRentals = await _context.Rentals
                    .Where(r => r.IsOverdue) 
                    .ToListAsync();          

            foreach (var rental in overdueRentals)
            {
                // Assuming rental has UserEmail property
                var emailBody = $"Dear Customer, your rental for {rental.BookId} is overdue.";
                //  await _emailService.SendEmailAsync(rental.UserId, "Overdue Rental Notification", emailBody);
                await _emailService.SendEmailAsync("KamalamMuthiah2016@gmail.com", "Overdue Rental Notification", emailBody);
            }
            await _context.SaveChangesAsync();
        }


      



    }
}
