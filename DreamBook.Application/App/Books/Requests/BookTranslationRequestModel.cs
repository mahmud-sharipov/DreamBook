using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Books
{
    public class BookTranslationRequestModel : ITranslationRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Book_Language))]
        public Guid LanguageGuid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Book_Name))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Book_Description))]
        public string Description { get; set; }
    }
}
