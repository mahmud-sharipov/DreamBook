using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Interpretations
{
    public class CreateInterpretationRequestModel : ITranslatableRequestModel<InterpretationTranslationRequestModel>
    {
        public Guid BookGuid { get; set; }
        public Guid WordGuid { get; set; }
        public virtual List<InterpretationTranslationRequestModel> Translations { get; set; }
    }
}
