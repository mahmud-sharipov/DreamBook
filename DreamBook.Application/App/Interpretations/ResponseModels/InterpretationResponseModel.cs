using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }
        public string Book { get; set; }
        public string Word { get; set; }
        public string Description { get; set; }
    }
}