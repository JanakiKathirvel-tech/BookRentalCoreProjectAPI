using BookRentalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.EFCore.DTO
{
    public class BookStatisticsDto
    {
        public Book MostOverdueBook { get; set; }
        public Book MostPopularBook { get; set; }
        public Book LeastPopularBook { get; set; }
    }
}
