using AutoMapper;
using Domain;
using Infrastructure;
using NSubstitute;
using System.Runtime.CompilerServices;

namespace BookLibrary.Tests
{
    public class UnitTests
    {
        private readonly IBookRepository _repository;
        private readonly IBookLibraryService _service;
        private readonly IMapper _mapper;
        public UnitTests()
        {
            _repository = Substitute.For<IBookRepository>();
            _mapper = Substitute.For<IMapper>();
            _service = new BookLibraryService(_repository, _mapper);
        }
        public void Test1()
        {
            var result = _service.GetBooksAsync("technology");
        }
    }
}