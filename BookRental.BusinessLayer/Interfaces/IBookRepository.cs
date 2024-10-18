using BookRental.EFCore.DTO;
using BookRentalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.BusinessLayer.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDTO>> Search(string name, string genre);

        Task<IEnumerable<Book>> GetAllBooks();

        Task<Book> GetBookbyId(int bookId);

        Task<BookStatisticsDto> GetBookStatisticsAsync();
    }
}
