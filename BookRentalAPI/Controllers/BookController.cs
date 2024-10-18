using BookRental.BusinessLayer.Interfaces;
using BookRental.BusinessLayer.Repositories;
using BookRental.EFCore;
using BookRental.EFCore.DTO;
using BookRentalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookRepository bookRepository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository bookRepository, ILogger<BookController> logger)
        {
            this.bookRepository = bookRepository;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet("GetAllBooks")]
        public async Task<IEnumerable<Book>> Get()
        {
            try
            {
                var result = await bookRepository.GetAllBooks();
                _logger.LogInformation("Get All Books ");
                return result;
            }
            catch(Exception)
            {
                _logger.LogError("No data in Book Table");
                return Enumerable.Empty<Book>();
            }
            
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> Search(string? name, string? gerne)
        {
            try
            {
              

                var result = await bookRepository.Search(name, gerne);

                if (result.Any())
                {
                    _logger.LogInformation($"Search for {name} is started");
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Search for {name} has error {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Search for {name} has some error..");
            }
        }

        [HttpGet("BookStatistics")]
        public async Task<ActionResult<BookStatisticsDto>> GetBookStatistics()
        {
            try
            {
                _logger.LogError("BookStatistics Get method started");
                var stats = await bookRepository.GetBookStatisticsAsync();
                return Ok(stats);
            }
            catch(Exception ex)
            {
                _logger.LogError($"BookStatistics has error {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data for Statistics ");
            }
            
        }

    }
}
