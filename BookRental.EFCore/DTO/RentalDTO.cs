using BookRentalAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.EFCore.DTO
{
    public class RentalDTO
    {
        
        public int RentalId { get; set; }
                
                
        public string UserName { get; set; }
        
        public string RentalDate { get; set; }
        public string ReturnDate { get; set; }        

        public string BookName { get; set; }
    }
}
