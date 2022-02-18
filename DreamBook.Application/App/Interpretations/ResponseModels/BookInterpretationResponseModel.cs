namespace DreamBook.Application.Interpretations
{
    public class BookInterpretationResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public string Word { get; set; }
        public Guid WordGuid { get; set; }

        public string Description { get; set; }
    }
}