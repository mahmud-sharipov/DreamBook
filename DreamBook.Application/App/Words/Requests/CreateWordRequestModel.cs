using System.Collections.Generic;

namespace DreamBook.Application.Words
{
    public class CreateWordRequestModel : ITranslatableRequestModel<WordTranslationRequestModel>
    {
        public List<WordTranslationRequestModel> Translations { get; set; }
    }
}
