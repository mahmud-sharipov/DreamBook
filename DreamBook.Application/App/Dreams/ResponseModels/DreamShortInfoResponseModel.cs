namespace DreamBook.Application.Dreams
{
    public class DreamShortInfoResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public bool CanBeShared { get; set; }
    }
}
