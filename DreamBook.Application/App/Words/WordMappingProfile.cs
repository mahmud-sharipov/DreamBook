using AutoMapper;
using DreamBook.Domain.Entities;

namespace DreamBook.Application.Words
{
    public class WordMappingProfile : Profile
    {
        public WordMappingProfile()
        {
            //Requests
            CreateMap<CreateWordRequestModel, Word>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<UpdateWordRequestModel, Word>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<WordTranslationRequestModel, WordTranslation>();

            //Responses
            CreateMap<Word, WordWithTranslationsResponseModel>();

            CreateMap<WordTranslation, WordResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.WordGuid));

            CreateMap<WordTranslation, WordTranslationResponseModel>();
        }
    }
}
