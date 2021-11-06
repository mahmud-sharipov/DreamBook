using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Books
{
    public class CreateBookRequestModel : ITranslatableRequestModel<BookTranslationRequestModel>
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Book_Translations))]
        public List<BookTranslationRequestModel> Translations { get; set; }
    }
}
