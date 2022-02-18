namespace DreamBook.Application.PostCategories
{
    public class PostCategoryResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
