using DreamBook.API.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.API.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();//Execute controller
            }
            else
            {
                var errorInModelState = context.ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse()
                {
                    ShortDescription = "One or more validation errors occurred",
                    Status = 400
                };

                foreach (var error in errorInModelState)
                {
                    var propertiValidations = new PropertyValidationErrorModel();
                    propertiValidations.PropertyName = error.Key;

                    foreach (var message in error.Value)
                        propertiValidations.Messages.Add(message);

                    errorResponse.Errors.Add(propertiValidations);
                }

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
