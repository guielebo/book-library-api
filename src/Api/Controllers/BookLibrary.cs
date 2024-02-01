using Domain;
using Domain.Models.Enuns;
using Microsoft.AspNetCore.Mvc;

namespace Torc.Assessment.BookLibrary.Controllers
{
    [ApiController]
    [Route("/book")]
    public class BookLibrary : ControllerBase
    {

        private readonly ILogger<BookLibrary> _logger;
        private readonly IBookLibraryService _service;
        public BookLibrary(IBookLibraryService service, ILogger<BookLibrary> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(string termSearch, SearchField searchField, int pageNumber, int pageSize)
        {
            var books = await _service.GetPaginatedBooksAsync(termSearch, searchField, pageNumber, pageSize);
            return Ok(books);
        }
    }
}
