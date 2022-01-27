using DreamBook.Domain.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DreamBook.Domain.Entities
{
    public class Book : EntityBase, ITranslatable<BookTranslation>
    {
        public Book()
        {
        }

        public virtual ICollection<BookTranslation> Translations { get; set; }

        public virtual ICollection<Interpretation> Interpretations { get; set; }
    }
}
