using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Books
{
    public class CreateBookRequestModel : ITranslatableRequestModel<BookTranslationRequestModel>
    {
        public List<BookTranslationRequestModel> Translations { get; set; }
    }
}
