using DreamBook.Application.Abstraction.Response;
using System;
using System.Collections.Generic;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Color { get; set; }

        public List<DreamTypeTranslationResponseModel> Translations { get; set; }
    }
}
