using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Books
{
    public class BookResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
