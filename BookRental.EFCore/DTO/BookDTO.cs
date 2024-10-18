using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.EFCore.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string ISDN { get; set; }
        public string Genre { get; set; }
    }
}
