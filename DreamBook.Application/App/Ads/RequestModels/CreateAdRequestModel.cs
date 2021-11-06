using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Ads
{
    public class CreateAdRequestModel : ITranslatableRequestModel<AdTranslationRequestModel>
    {
        public string Image { get; set; }
        public string Source { get; set; }
        public List<AdTranslationRequestModel> Translations { get; set; }
    }
}