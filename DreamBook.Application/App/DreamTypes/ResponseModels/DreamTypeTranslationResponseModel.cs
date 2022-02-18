using DreamBook.Application.Languages;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeTranslationResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public LanguageShortResponseModel Language { get; set; }
    }
}
