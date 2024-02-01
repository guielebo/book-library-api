using Domain.Models.Enuns;
using Infrastructure.Entities;

namespace Infrastructure
{
    public interface IBookRepository
    {
        Task<PaginatedResult<Book>> GetPaginatedBooksAsync(string searchTerm, SearchField searchField, int pageNumber, int pageSize);
    }

}
