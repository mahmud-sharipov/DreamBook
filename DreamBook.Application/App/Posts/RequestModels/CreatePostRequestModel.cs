namespace DreamBook.Application.Posts
{
    public class CreatePostRequestModel : IRequestModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryGuid { get; set; }
    }
}
