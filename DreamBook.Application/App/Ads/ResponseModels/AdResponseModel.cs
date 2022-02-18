namespace DreamBook.Application.Ads
{
    public class AdResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        public string CreatedAt { get; set; }

        public string Source { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
