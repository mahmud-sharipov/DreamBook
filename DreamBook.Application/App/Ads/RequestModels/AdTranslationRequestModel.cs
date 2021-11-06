using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Ads
{
    public class AdTranslationRequestModel : ITranslationRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid LanguageGuid { get; set; }
    }
}
