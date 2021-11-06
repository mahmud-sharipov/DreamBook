using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Books
{
    public class BookWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public List<BookTranslationResponseModel> Translations { get; set; }
    }
}
