using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public virtual List<InterpretationTranslationResponseModel> Translations { get; set; }
    }
}
