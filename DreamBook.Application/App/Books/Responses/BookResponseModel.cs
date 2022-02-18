namespace DreamBook.Application.Books
{
    public class BookResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
