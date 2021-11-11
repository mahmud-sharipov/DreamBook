using System;

namespace DreamBook.Domain.Entities
{
    public class Post : EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Image { get; set; }
        public bool IsActive { get; set; }

        public Guid CategoryGuid { get; set; }
        public virtual PostCategory Category { get; set; }
    }
}
