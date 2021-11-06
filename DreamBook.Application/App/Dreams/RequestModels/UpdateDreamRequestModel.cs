using System;

namespace DreamBook.Application.Dreams
{
    public class UpdateDreamRequestModel : CreateDreamRequestModel
    {
        public Guid Guid { get; set; }
    }
}
