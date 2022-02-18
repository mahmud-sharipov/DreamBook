using System.Collections.Generic;

namespace DreamBook.Application.Books
{
    public class CreateBookRequestModel : ITranslatableRequestModel<BookTranslationRequestModel>
    {
        public List<BookTranslationRequestModel> Translations { get; set; }
    }
}
