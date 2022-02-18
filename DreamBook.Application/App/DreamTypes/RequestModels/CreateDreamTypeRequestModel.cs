using System.Collections.Generic;

namespace DreamBook.Application.DreamTypes
{
    public class CreateDreamTypeRequestModel : ITranslatableRequestModel<DreamTypeTranslationRequestModel>
    {
        public string Color { get; set; }
        public List<DreamTypeTranslationRequestModel> Translations { get; set; }
    }
}
