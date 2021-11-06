using AutoMapper;
using DreamBook.Domain.Entities;

namespace DreamBook.Application.PostCategories
{
    public class PostCategoryMappingProfile : Profile
    {
        public PostCategoryMappingProfile()
        {
            //Requests
            CreateMap<CreatePostCategoryRequestModel, PostCategory>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<UpdatePostCategoryRequestModel, PostCategory>()
                .ForMember(d => d.Translations, s => s.Ignore());
            CreateMap<PostCategoryTranslationRequestModel, PostCategoryTranslation>();

            //Responses
            CreateMap<PostCategory, PostCategoryWithTranslationsResponseModel>();

            CreateMap<PostCategoryTranslation, PostCategoryResponseModel>()
                .ForMember(des => des.Guid, opt => opt.MapFrom(src => src.CategoryGuid));

            CreateMap<PostCategoryTranslation, PostCategoryTranslationResponseModel>();
        }
    }
}
