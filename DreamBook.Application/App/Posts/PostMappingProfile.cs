using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using System.Linq;

namespace DreamBook.Application.Posts
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            //Requests
            CreateMap<CreatePostRequestModel, Post>();
            CreateMap<UpdatePostRequestModel, Post>();

            //Responses
            CreateMap<Post, PostResponseModel>()
                .ForMember(des => des.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Translations.Single(t => t.LanguageGuid == AppLanguageManager.CurrentAppLanguage.Guid).Name));
        }
    }
}
