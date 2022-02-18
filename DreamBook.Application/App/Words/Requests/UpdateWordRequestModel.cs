namespace DreamBook.Application.Words
{
    public class UpdateWordRequestModel : CreateWordRequestModel
    {
        public Guid Guid { get; set; }
    }
}
