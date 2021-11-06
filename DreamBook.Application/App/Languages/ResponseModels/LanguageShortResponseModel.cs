using DreamBook.Application.Abstraction.Response;
using System;

namespace DreamBook.Application.Languages
{
    public class LanguageShortResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }
    }
}
