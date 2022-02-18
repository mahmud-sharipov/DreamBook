namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeMappingProfile : Profile
    {
        public DreamTypeMappingProfile() : base()
        {
            //Requests
            CreateMap<CreateDreamTypeRequestModel, DreamType>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<UpdateDreamTypeRequestModel, DreamType>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<DreamTypeTranslationRequestModel, DreamTypeTranslation>();

            //Responses
            CreateMap<DreamType, DreamTypeWithTranslationsResponseModel>();

            CreateMap<DreamTypeTranslation, DreamTypeResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.DreamTypeGuid))
                .ForMember(des => des.Color, opt => opt.MapFrom(src => src.DreamType.Color));

            CreateMap<DreamTypeTranslation, DreamTypeTranslationResponseModel>();
        }
    }
}
