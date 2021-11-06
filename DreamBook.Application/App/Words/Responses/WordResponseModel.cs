using DreamBook.Application.Abstraction.Response;
using System;

namespace DreamBook.Application.Words
{
    public class WordResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }
    }
}
