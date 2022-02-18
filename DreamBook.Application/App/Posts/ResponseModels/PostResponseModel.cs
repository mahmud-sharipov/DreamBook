namespace DreamBook.Application.Posts
{
    public class PostResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public Guid CategoryGuid { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
