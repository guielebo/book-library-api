using Domain;
using Infrastructure;

namespace BookLibrary.Tests
{
    using Api.AutoMapper;
    using AutoMapper;
    using Domain.Models;
    using Domain.Models.Enuns;
    using Infrastructure.Entities;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class BookLibraryServiceTests
    {
        private readonly IBookRepository _repository = Substitute.For<IBookRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly BookLibraryService _service;

        public BookLibraryServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<BookProfile>());

            _mapper = new Mapper(config);

            _service = new BookLibraryService(_repository, _mapper);           
        }

        [Fact]
        public async Task GetPaginatedBooksAsync_WithSearchTerm_ReturnsBooks()
        {
            // Arrange
            var searchTerm = "Dune";
            var mockBooks = new List<Book>
        {
            new Book { BookId = 1, Title = "Dune", ISBN = "123", Category = "Sci-Fi" }
        };
            var paginatedBooks = new PaginatedResult<Book>
            {
                Items = mockBooks,
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 1
            };

            _repository.GetPaginatedBooksAsync(searchTerm, SearchField.Title, 1, 1)
                .Returns(Task.FromResult(paginatedBooks));

            // Act
            var result = await _service.GetPaginatedBooksAsync(searchTerm, SearchField.Title, 1, 1);

            // Assert
            Assert.Single(result.Items);
            Assert.Equal("Dune", result.Items.First().Title);
        }

        [Fact]
        public async Task GetPaginatedBooksAsync_NoSearchTerm_ReturnsAllBooks()
        {
            // Arrange
            var mockBooks = new List<Book>
        {
            new Book { BookId = 1, Title = "Dune", ISBN = "123", Category = "Sci-Fi" },
            new Book { BookId = 2, Title = "1984", ISBN = "456", Category = "Fiction" }
        };
            var paginatedBooks = new PaginatedResult<Book>
            {
                Items = mockBooks,
                TotalCount = 2,
                PageNumber = 1,
                PageSize = 2
            };

            _repository.GetPaginatedBooksAsync("", SearchField.None, 1, 2)
                .Returns(Task.FromResult(paginatedBooks));

            // Act
            var result = await _service.GetPaginatedBooksAsync("", SearchField.None, 1, 2);

            // Assert
            Assert.Equal(2, result.Items.Count);
            Assert.Contains(result.Items, b => b.Title == "Dune");
            Assert.Contains(result.Items, b => b.Title == "1984");
        }

        [Fact]
        public async Task GetPaginatedBooksAsync_EmptyRepository_ReturnsEmptyResult()
        {
            // Arrange
            _repository.GetPaginatedBooksAsync(Arg.Any<string>(), Arg.Any<SearchField>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(Task.FromResult(new PaginatedResult<Book>
                {
                    Items = new List<Book>(),
                    TotalCount = 0,
                    PageNumber = 1,
                    PageSize = 1
                }));

            // Act
            var result = await _service.GetPaginatedBooksAsync("NonExisting", SearchField.Title, 1, 1);

            // Assert
            Assert.Empty(result.Items);
        }
    }

}