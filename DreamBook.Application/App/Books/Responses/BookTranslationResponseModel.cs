using DreamBook.Application.LanguageResources;
using DreamBook.Application.Languages;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Books
{
    public class BookTranslationResponseModel : BookResponseModel
    {
        public LanguageShortResponseModel Language { get; set; }
    }
}
