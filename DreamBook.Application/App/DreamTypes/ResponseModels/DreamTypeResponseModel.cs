using DreamBook.Application.Abstraction.Response;
using System;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Color { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
