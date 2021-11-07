using DreamBook.Application.Languages;

namespace DreamBook.Application.Books
{
    public class BookTranslationResponseModel : BookResponseModel
    {
        public LanguageShortResponseModel Language { get; set; }
    }
}
