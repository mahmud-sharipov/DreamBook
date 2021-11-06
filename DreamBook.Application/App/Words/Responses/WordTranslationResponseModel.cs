using DreamBook.Application.Languages;

namespace DreamBook.Application.Words
{
    public class WordTranslationResponseModel : WordResponseModel
    {
        public LanguageShortResponseModel Language { get; set; }
    }
}
