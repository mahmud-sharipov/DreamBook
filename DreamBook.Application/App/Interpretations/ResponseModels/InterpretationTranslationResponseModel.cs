using DreamBook.Application.Languages;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationTranslationResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Description { get; set; }

        public LanguageResponseModel Language { get; set; }
    }
}
