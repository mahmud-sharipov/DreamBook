using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
