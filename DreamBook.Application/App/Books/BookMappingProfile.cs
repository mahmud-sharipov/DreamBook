using AutoMapper;
using DreamBook.Domain.Entities;

namespace DreamBook.Application.Books
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            //Requests
            CreateMap<CreateBookRequestModel, Book>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<UpdateBookRequestModel, Book>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<BookTranslationRequestModel, BookTranslation>();

            //Responses
            CreateMap<Book, BookWithTranslationsResponseModel>();

            CreateMap<BookTranslation, BookResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.BookGuid));

            CreateMap<BookTranslation, BookTranslationResponseModel>();
        }
    }
}
