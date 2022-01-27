using DreamBook.Domain.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DreamBook.Domain.Entities
{
    public class Word : EntityBase, ITranslatable<WordTranslation>
    {
        public Word()
        {
        }

        public virtual ICollection<WordTranslation> Translations { get; set; }

        public virtual ICollection<Interpretation> Interpretations { get; set; }

        public virtual ICollection<DreamWord> Dreams { get; set; }
    }
}
