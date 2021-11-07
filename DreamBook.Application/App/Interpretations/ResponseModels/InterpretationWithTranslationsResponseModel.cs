using DreamBook.Application.Abstraction.Response;
using System;
using System.Collections.Generic;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public virtual List<InterpretationTranslationResponseModel> Translations { get; set; }
    }
}
