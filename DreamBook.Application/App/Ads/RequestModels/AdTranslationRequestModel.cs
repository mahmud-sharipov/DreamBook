﻿namespace DreamBook.Application.Ads
{
    public class AdTranslationRequestModel : ITranslationRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid LanguageGuid { get; set; }
    }
}
