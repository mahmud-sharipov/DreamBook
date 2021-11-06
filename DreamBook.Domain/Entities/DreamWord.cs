using System;

namespace DreamBook.Domain.Entities
{
    public class DreamWord : EntityBase
    {
        public Guid DreamGuid { get; set; }
        public virtual Dream Dream { get; set; }

        public Guid WordGuid { get; set; }
        public virtual Word Word { get; set; }
    }
}
