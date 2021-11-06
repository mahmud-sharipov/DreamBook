using DreamBook.Domain.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DreamBook.Domain.Entities
{
    public class PostCategory : EntityBase, ITranslatable<PostCategoryTranslation>
    {
        public PostCategory()
        {
            Posts = new Collection<Post>();
            Translations = new Collection<PostCategoryTranslation>();
        }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostCategoryTranslation> Translations { get; set; }
    }
}
