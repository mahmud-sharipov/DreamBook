using DreamBook.Domain.Interfaces;
using System;

namespace DreamBook.Domain.Entities
{
    public class DreamTypeTranslation : EntityBase, ITranslation
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid DreamTypeGuid { get; set; }
        public virtual DreamType DreamType { get; set; }

        public Guid LanguageGuid { get; set; }
        public virtual Language Language { get; set; } 
    }
}
