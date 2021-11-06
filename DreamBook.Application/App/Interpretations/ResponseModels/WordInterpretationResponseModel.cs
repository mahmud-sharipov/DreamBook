using DreamBook.Application.Abstraction.Response;
using System;

namespace DreamBook.Application.Interpretations
{
    public class WordInterpretationResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Book { get; set; }
        public Guid BookGuid { get; set; }

        public string Description { get; set; }
    }
}