using DreamBook.Application.Abstraction.Request;
using System.Collections.Generic;

namespace DreamBook.Application.Ads
{
    public class CreateAdRequestModel : ITranslatableRequestModel<AdTranslationRequestModel>
    {
        public string Image { get; set; }
        public string Source { get; set; }
        public bool IsActive { get; set; }
        public List<AdTranslationRequestModel> Translations { get; set; }
    }
}