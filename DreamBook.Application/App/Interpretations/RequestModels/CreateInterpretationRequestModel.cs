using DreamBook.Application.Abstraction.Request;
using System;
using System.Collections.Generic;

namespace DreamBook.Application.Interpretations
{
    public class CreateInterpretationRequestModel : ITranslatableRequestModel<InterpretationTranslationRequestModel>
    {
        public Guid BookGuid { get; set; }
        public Guid WordGuid { get; set; }
        public virtual List<InterpretationTranslationRequestModel> Translations { get; set; }
    }
}
