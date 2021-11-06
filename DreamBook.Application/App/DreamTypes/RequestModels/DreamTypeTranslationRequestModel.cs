using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeTranslationRequestModel : ITranslationRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LanguageGuid { get; set; }
    }
}