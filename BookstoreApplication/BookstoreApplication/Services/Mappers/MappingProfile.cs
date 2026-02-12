using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;

namespace BookstoreApplication.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.PublishedBefore,
                opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year)
            )
            .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => src.Author.FullName)
            )
            .ForMember(
                dest => dest.Publisher,
                opt => opt.MapFrom(src => src.Publisher.Name)
            ).ReverseMap();
        CreateMap<Book, BookDetailsDto>()
            .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => src.Author.FullName)
            )
            .ForMember(
                dest => dest.Publisher,
                opt => opt.MapFrom(src => src.Publisher.Name)
            ).ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();

    }
    
}