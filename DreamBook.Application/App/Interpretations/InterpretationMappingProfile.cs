namespace DreamBook.Application.Interpretations
{
    public class InterpretationMappingProfile : Profile
    {
        public InterpretationMappingProfile()
        {
            //Requests
            CreateMap<CreateInterpretationRequestModel, Interpretation>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<UpdateInterpretationRequestModel, Interpretation>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<InterpretationTranslationRequestModel, InterpretationTranslation>();

            //Responses
            CreateMap<Interpretation, InterpretationWithTranslationsResponseModel>();

            CreateMap<InterpretationTranslation, InterpretationResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.InterpretationGuid))
                .ForMember(des => des.Word, opt => opt.MapFrom(src => src.Word.Name))
                .ForMember(des => des.Book, opt => opt.MapFrom(src => src.Book.Name));

            CreateMap<InterpretationTranslation, WordInterpretationResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.InterpretationGuid))
                .ForMember(des => des.BookGuid, opt => opt.MapFrom(src => src.Book.Guid))
                .ForMember(des => des.Book, opt => opt.MapFrom(src => src.Book.Name));

            CreateMap<InterpretationTranslation, BookInterpretationResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.InterpretationGuid))
                .ForMember(des => des.WordGuid, opt => opt.MapFrom(src => src.Word.WordGuid))
                .ForMember(des => des.Word, opt => opt.MapFrom(src => src.Word.Name));

            CreateMap<InterpretationTranslation, InterpretationTranslationResponseModel>();
        }
    }
}
