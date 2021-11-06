namespace DreamBook.API.Auth.Requests
{
    public class GoogleAuthRequest
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
