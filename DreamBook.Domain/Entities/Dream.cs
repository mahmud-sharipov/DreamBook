using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DreamBook.Domain.Entities
{
    public class Dream : EntityBase
    {
        public Dream()
        {
            Words = new Collection<DreamWord>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Weather { get; set; }
        public DateTime DateTime { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public string Image { get; set; }
        public int NumberOfViews { get; set; }
        public bool CanBeShared { get; set; }

        public Guid AuthorGuid { get; set; }
        public virtual User Author { get; set; }

        public Guid TypeGuid { get; set; }
        public virtual DreamType Type { get; set; }

        public virtual ICollection<DreamWord> Words { get; set; }
    }
}
