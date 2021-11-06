using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Books
{
    public class UpdateBookRequestModel : CreateBookRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.EntityGuid))]
        public Guid Guid { get; set; }
    }
}
