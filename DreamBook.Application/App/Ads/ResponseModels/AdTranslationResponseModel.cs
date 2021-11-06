using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.Languages;
using System;

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
