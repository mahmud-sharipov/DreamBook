using System.Collections.Generic;

namespace DreamBook.Application.Ads
{
    public class AdWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Image { get; set; }

        public string CreatedAt { get; set; }

        public string Source { get; set; }

        public IList<AdTranslationResponseModel> Translations { get; set; }
    }
}
