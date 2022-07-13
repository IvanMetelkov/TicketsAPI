using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.Threading.Tasks;
using tickets.Validation;
using Microsoft.AspNetCore.Http;
using System.Text;
using tickets.Exceptions;

namespace tickets.Filters
{
    public class ValidationFilter : IAsyncResourceFilter
    {
        private readonly IValidator _validator;

        public ValidationFilter(IValidator validator)
        {
            _validator = validator;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            context.HttpContext.Request.EnableBuffering();
            var reader = new StreamReader(
                context.HttpContext.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: -1,
                leaveOpen: true);
            string jsonString = await reader.ReadToEndAsync();
            context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            string path = context.HttpContext.Request.Path;
            string operation = path.Substring(path.LastIndexOf('/') + 1);

            if (!await _validator.ValidateJsonAsync(jsonString, operation))
            {
                throw new JsonValidationException("provided json is of unsupported format");
            }
            await next();
        }
    }
}
