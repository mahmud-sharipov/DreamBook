using DreamBook.Domain.Enums;
using DreamBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DreamBook.Domain.Entities
{
    public class User : EntityBase, IUser
    {
        public User()
        {
            Dreams = new Collection<Dream>();
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string AvatarImage { get; set; }

        public virtual ICollection<Dream> Dreams { get; set; }
    }
}
