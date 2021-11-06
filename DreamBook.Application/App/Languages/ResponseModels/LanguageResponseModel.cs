using DreamBook.Application.Abstraction.Response;
using System;

namespace DreamBook.Application.Languages
{
    public class LanguageResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsDefault { get; set; }
    }
}
