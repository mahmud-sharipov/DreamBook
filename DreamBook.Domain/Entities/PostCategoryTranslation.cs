using DreamBook.Domain.Interfaces;
using System;

namespace DreamBook.Domain.Entities
{
    public class PostCategoryTranslation : EntityBase, ITranslation
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid LanguageGuid { get; set; }
        public virtual Language Language { get; set; }

        public Guid CategoryGuid { get; set; }
        public virtual PostCategory PostCategory { get; set; }
    }
}
