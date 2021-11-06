using DreamBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DreamBook.Domain.Entities
{
    public class WordTranslation : EntityBase, ITranslation
    {
        public WordTranslation()
        {
            Interpretations = new Collection<InterpretationTranslation>();
        }

        public Guid WordGuid { get; set; }
        public virtual Word Word { get; set; }

        public Guid LanguageGuid { get; set; }
        public virtual Language Language { get; set; }

        public string Name { get; set; }

        public virtual ICollection<InterpretationTranslation> Interpretations { get; set; }

    }
}
