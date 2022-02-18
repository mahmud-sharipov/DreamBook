using DreamBook.Application.Ads;
using DreamBook.Application.Books;
using DreamBook.Application.Dreams;
using DreamBook.Application.DreamTypes;
using DreamBook.Application.Interpretations;
using DreamBook.Application.Languages;
using DreamBook.Application.PostCategories;
using DreamBook.Application.Posts;
using DreamBook.Application.Words;
using Microsoft.Extensions.DependencyInjection;

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
                typeof(WordMappingProfile));

            service.AddScoped<AppLanguageManager>();
            service.AddScoped<IAdService, AdService>();
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<IDreamService, DreamService>();
            service.AddScoped<IDreamTypeService, DreamTypeService>();
            service.AddScoped<IInterpretationService, InterpretationService>();
            service.AddScoped<IPostCategoryService, PostCategoryService>();
            service.AddScoped<IPostService, PostService>();
            service.AddScoped<IWordService, WordService>();

            return service;
        }
    }
}
