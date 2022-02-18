namespace DreamBook.Application.Books
{
    public class UpdateBookRequestModel : CreateBookRequestModel
    {
        public Guid Guid { get; set; }
    }
}
