using BookRental.EFCore;
using BookRentalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.BusinessLayer
{
    public class RentalService
    {
        private readonly BookRentalDBContext _context;

        public RentalService(BookRentalDBContext context)
        {
            _context = context;
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

        public async Task<List<Rental>> GetOverdueRentalsAsync()
        {
            return await _context.Rentals
                .Where(r => r.IsOverdue && r.ReturnDate == null)
                .ToListAsync();
        }
    }
}
