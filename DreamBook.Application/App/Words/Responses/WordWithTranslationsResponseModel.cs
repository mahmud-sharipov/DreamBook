using System.Collections.Generic;

namespace DreamBook.Application.Words
{
    public class WordWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public List<WordTranslationResponseModel> Translations { get; set; }
    }
}
