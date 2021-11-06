using AutoMapper;
using DreamBook.Domain.Entities;

namespace DreamBook.Application.Languages
{
    public class LanguageMappingProfile : Profile
    {
        public LanguageMappingProfile()
        {
            //Responses
            CreateMap<Language, LanguageResponseModel>();
            CreateMap<Language, LanguageShortResponseModel>();
        }
    }
}
