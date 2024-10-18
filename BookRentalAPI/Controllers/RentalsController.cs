using BookRental.BusinessLayer;
using BookRental.BusinessLayer.Interfaces;
using BookRental.EFCore;
using BookRental.EFCore.DTO;
using BookRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NuGet.Protocol;
using System;
using System.Net;

namespace BookRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly BookRentalDBContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly ILogger<RentalsController> _logger;

        public RentalsController(BookRentalDBContext context, IBookRepository bookRepository, IRentalRepository rentalRepository, ILogger<RentalsController> logger)
        {
            _context = context;
            _bookRepository = bookRepository;
            _rentalRepository = rentalRepository;
            _logger = logger;
        }

        [HttpPost("rent/{bookId},{userId}")]
        public async Task<ActionResult> RentBook(int bookId, int userId)
        {

            try
            {
                if (bookId == 0)
                {
                    return BadRequest("BookId is Required");
                }

                _logger.LogInformation($"Post method for rent of Book of Id {bookId}");
                var bookToRental = await _bookRepository.GetBookbyId(bookId);

                if (bookToRental == null)
                    return NotFound($"Book with Id = {bookId} not found");

                
                if (!bookToRental.IsAvailable)
                {
                    return NotFound($"This book is not available for rent");
                }


                var rent = await _rentalRepository.RentBook(bookId, userId);
                return Ok("Book rented successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post method for rent of Book of Id {bookId} has error {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error renting book");
            }

        }

        [HttpPost("return/{rentalId}")]
        public async Task<ActionResult> ReturnBook(int rentalId)
        {
            try
            {
                _logger.LogInformation($"Post method for return of Book of RentalId {rentalId}");
                var rental = await _rentalRepository.GetRentalbyId(rentalId);
                if (rental == null)
                   return  NotFound($"BookRental with Id = {rentalId} not found");

               rental = await _rentalRepository.ReturnBook(rentalId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post method for return of Book of RentalId {rentalId} error {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Returning book");
            }
            return Ok("Book returned successfully");
        }

        // GET: api/RentalHistory
        [HttpGet("ViewRentalHistory")]
        public async Task<IEnumerable<RentalDTO>> Get()
        {
            return await _rentalRepository.GetAllRentalBooks();
        }


        [HttpPost("MarkOverdue")]
        public async Task<ActionResult> MarkOverdueBooksRental()
        {
            try
            {
                _logger.LogInformation($"Post method MarkOverdue");                
                await _rentalRepository.MarkOverdueRentalsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post method MarkOverdue error {ex.Message}");                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Markoverdue book");
            }
            return Ok("Books marked overdue successfully");
        }

        [HttpPost("SendMailNotification")]
        public async Task<ActionResult> SendOverdueBooksRental()
        {
            try
            {
                _logger.LogInformation($"SendMailNotification method Started");
                await _rentalRepository.SendMailOverdueNotificationsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendMailNotification method erorr {ex.Message}");
               // return BadRequest(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error MailNotification for overdue book");
            }
            return Ok("Overdue Notifications sent successfully");
        }



    }
}
