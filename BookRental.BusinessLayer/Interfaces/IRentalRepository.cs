using BookRental.EFCore.DTO;
using BookRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.BusinessLayer.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> RentBook(int bookId, int userId);

        Task<Rental> ReturnBook(int returnId);


        Task<IEnumerable<RentalDTO>> GetAllRentalBooks();

        Task<Rental> GetRentalbyId(int rentalId);

        Task MarkOverdueRentalsAsync();

        Task SendMailOverdueNotificationsAsync();





    }
}
