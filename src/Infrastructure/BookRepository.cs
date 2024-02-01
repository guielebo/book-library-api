using Domain.Models.Enuns;
using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly MyDbContext _context;

        public BookRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Book>> GetPaginatedBooksAsync(string searchTerm, SearchField searchField, int pageNumber, int pageSize)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchField)
                {
                    case SearchField.Title:
                        query = query.Where(b => b.Title.Contains(searchTerm));
                        break;
                    case SearchField.AuthorFirstName:
                        query = query.Where(b => b.FirstName.Contains(searchTerm));
                        break;
                    case SearchField.AuthorLastName:
                        query = query.Where(b => b.LastName.Contains(searchTerm));
                        break;
                    case SearchField.ISBN:
                        query = query.Where(b => b.ISBN == searchTerm);
                        break;
                    case SearchField.Category:
                        query = query.Where(b => b.Category == searchTerm);
                        break;
                    case SearchField.None:
                        query = query.Where(b => b.Title.Contains(searchTerm) ||
                                                 b.FirstName.Contains(searchTerm) ||
                                                 b.LastName.Contains(searchTerm) ||
                                                 b.ISBN.Contains(searchTerm) ||
                                                 b.Category.Contains(searchTerm));
                        break;
                }
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Book>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}
