using DreamBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DreamBook.Domain.Entities
{
    public class BookTranslation : EntityBase, ITranslation
    {
        public BookTranslation()
        {
        }

        public Guid BookGuid { get; set; }
        public virtual Book Book { get; set; }

        public Guid LanguageGuid { get; set; }
        public virtual Language Language { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<InterpretationTranslation> Interpretations { get; set; }
    }
}
