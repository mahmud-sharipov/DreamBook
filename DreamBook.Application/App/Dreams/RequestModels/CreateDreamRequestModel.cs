using DreamBook.Application.Abstraction.Request;
using System;
using System.Collections.Generic;

namespace DreamBook.Application.Dreams
{
    public class DreamRequestModel : IRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Weather { get; set; }
        public DateTime DateTime { get; set; }
        public string Image { get; set; }
        public bool CanBeShared { get; set; }
        public Guid TypeGuid { get; set; }
    }

    public class CreateDreamRequestModel : DreamRequestModel
    {
        public List<Guid> Words { get; set; }
    }
}
