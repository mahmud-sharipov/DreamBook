using DreamBook.Application.Languages;

namespace DreamBook.Application.Ads
{
    public class AdTranslationResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LanguageShortResponseModel Language { get; set; }
    }
}
