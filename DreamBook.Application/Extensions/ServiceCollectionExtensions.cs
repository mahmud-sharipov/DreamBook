using DreamBook.Application.Abstraction;
using DreamBook.Application.Ads;
using DreamBook.Application.Books;
using DreamBook.Application.PostCategories;
using DreamBook.Application.Words;
using DreamBook.Application.DreamTypes;
using DreamBook.Application.Interpretations;
using DreamBook.Application.Languages;
using DreamBook.Application.Posts;
using DreamBook.Application.Users;
using Microsoft.Extensions.DependencyInjection;
using DreamBook.Application.Dreams;

namespace DreamBook.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(
                typeof(AdMappingProfile),
                typeof(BookMappingProfile),
                typeof(DreamTypeMappingProfile),
                typeof(DreamMappingProfile),
                typeof(InterpretationMappingProfile),
                typeof(LanguageMappingProfile),
                typeof(PostCategoryMappingProfile),
                typeof(PostMappingProfile),
                typeof(UserMappingProfile),
                typeof(WordMappingProfile));

            service.AddScoped<AppLanguageManager>();
            service.AddScoped<IAdService, AdService>();
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<IDreamService, DreamService>();
            service.AddScoped<IDreamTypeService, DreamTypeService>();
            service.AddScoped<IInterpretationService, InterpretationService>();
            service.AddScoped<IPostCategoryService, PostCategoryService>();
            service.AddScoped<IPostService, PostService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IWordService, WordService>();

            return service;
        }
    }
}
