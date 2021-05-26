using AutoMapper;
using BookStore.API.Data;
using BookStore.API.DTOs;

namespace BookStore.API.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        } 
    }
}
