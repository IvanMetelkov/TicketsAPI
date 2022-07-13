using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Schema;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace tickets.Validation
{
    public class Validator : IValidator
    {
        private readonly IMemoryCache _cache;

        public Validator(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> ValidateJsonAsync(string jsonString, string operation)
        {
            AsyncLazy<JSchema> schema = null;
            if (operation != null)
            {
                _cache.TryGetValue(operation, out schema);
            }

            if (schema != null)
            {
                JObject request = JObject.Parse(jsonString);

                return request.IsValid(await schema);
            }

            return false;
        }
    }
}