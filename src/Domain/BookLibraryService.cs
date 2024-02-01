using AutoMapper;
using Domain.Models;
using Domain.Models.Enuns;
using Infrastructure;
using Infrastructure.Entities;

namespace Domain
{
    public class BookLibraryService : IBookLibraryService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookLibraryService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BookModel>> GetPaginatedBooksAsync(string termSearch, SearchField searchField, int pageNumber, int pageSize)
        {
            var response = await _bookRepository.GetPaginatedBooksAsync(termSearch, searchField, pageNumber, pageSize);
            var bookModels = _mapper.Map<ICollection<BookModel>>(response.Items);

            return new PaginatedResult<BookModel>
            {
                Items = bookModels,
                TotalCount = response.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
