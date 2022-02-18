namespace DreamBook.Application.Ads
{
    public class AdMappingProfile : Profile
    {
        public AdMappingProfile()
        {
            //Requests
            CreateMap<CreateAdRequestModel, Ad>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<UpdateAdRequestModel, Ad>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<AdTranslationRequestModel, AdTranslation>();

            //Responses
            CreateMap<Ad, AdWithTranslationsResponseModel>();

            CreateMap<AdTranslation, AdResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.AdGuid))
                .ForMember(des => des.Source, opt => opt.MapFrom(src => src.Ad.Source))
                .ForMember(des => des.CreatedAt, opt => opt.MapFrom(src => src.Ad.CreatedAt))
                .ForMember(des => des.IsActive, opt => opt.MapFrom(src => src.Ad.IsActive))
                .ForMember(des => des.Image, opt => opt.MapFrom(src => src.Ad.Image));

            CreateMap<AdTranslation, AdTranslationResponseModel>();
        }
    }
}
