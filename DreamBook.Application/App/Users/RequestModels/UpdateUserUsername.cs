using System.Text.Json.Serialization;

namespace DreamBook.Application.Users;

public class UpdateUserUsernameRequestModel : IRequestModel
{
    [JsonIgnore]
    public Guid Guid { get; set; }
    public string UserName { get; set; }
}
