using DreamBook.Application.Abstraction.Response;
using System;
using System.Collections.Generic;

namespace DreamBook.Application.Books
{
    public class BookWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public List<BookTranslationResponseModel> Translations { get; set; }
    }
}
