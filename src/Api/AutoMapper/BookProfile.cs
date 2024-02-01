using AutoMapper;
using Domain.Models;
using Infrastructure.Entities;

namespace Api.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
