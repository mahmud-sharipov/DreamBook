using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.DreamTypes
{
    public class CreateDreamTypeRequestModel : ITranslatableRequestModel<DreamTypeTranslationRequestModel>
    {
        public string Color { get; set; }
        public List<DreamTypeTranslationRequestModel> Translations { get; set; }
    }
}
