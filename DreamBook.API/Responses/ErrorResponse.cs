using Newtonsoft.Json;
using System.Collections.Generic;

namespace DreamBook.API.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new List<PropertyValidationErrorModel>();
        }

        public string ShortDescription { get; set; }

        public int Status { get; set; }

        public IList<PropertyValidationErrorModel> Errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
