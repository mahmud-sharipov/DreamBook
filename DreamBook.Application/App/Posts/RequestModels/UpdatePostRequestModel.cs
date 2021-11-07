using System;

namespace DreamBook.Application.Posts
{
    public class UpdatePostRequestModel : CreatePostRequestModel
    {
        public Guid Guid { get; set; }
    }
}
