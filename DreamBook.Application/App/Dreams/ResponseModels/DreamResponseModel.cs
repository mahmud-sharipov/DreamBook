using DreamBook.Application.DreamTypes;
using System.Collections.Generic;

namespace DreamBook.Application.Dreams
{
    public class DreamResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Weather { get; set; }
        public DateTime DateTime { get; set; }
        public string Image { get; set; }
        public bool CanBeShared { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int NumberOfViews { get; set; }
        public virtual DreamTypeResponseModel Type { get; set; }

        public IEnumerable<string> Words { get; set; }
    }
}
