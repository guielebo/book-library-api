using Domain.Models;
using Domain.Models.Enuns;
using Infrastructure.Entities;

namespace Domain
{
    public interface IBookLibraryService
    {
        Task<PaginatedResult<BookModel>> GetPaginatedBooksAsync(string termSearch, SearchField searchField, int pageNumber, int pageSize);
    }
}
