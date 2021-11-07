using Newtonsoft.Json;
using System.Collections.Generic;

namespace DreamBook.API.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            PropertyValidations = new List<PropertyValidationErrorModel>();
        }

        public string Title { get; set; }

        public string Error { get; set; }

        public int Status { get; set; }

        public IList<PropertyValidationErrorModel> PropertyValidations { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
