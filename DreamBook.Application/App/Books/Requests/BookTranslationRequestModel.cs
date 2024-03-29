﻿using DreamBook.Application.Abstraction.Request;
using System;

namespace DreamBook.Application.Books
{
    public class BookTranslationRequestModel : ITranslationRequestModel
    {
        public Guid LanguageGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
