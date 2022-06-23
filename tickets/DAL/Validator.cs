using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using tickets.DTO;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace tickets.DAL
{
    public class Validator : IValidator
    {
        private readonly IMemoryCache _cache;
        private readonly IDictionary<string, string> _schemaPaths;

        public Validator(IMemoryCache cache, IDictionary<string, string> schemaPaths)
        {
            _cache = cache;
            _schemaPaths = schemaPaths;
        }

        public async Task<bool> ValidateDTOAsync(IRequestDTO requestDTO)
        {
            JSchema schema = null;
            if (requestDTO.OperationType != null && !_cache.TryGetValue(requestDTO.OperationType, out schema))
            {
                if (_schemaPaths.TryGetValue(requestDTO.OperationType, out string path))
                {
                    string schemaString = await File.ReadAllTextAsync(path);
                    schema = JSchema.Parse(schemaString);
                    _cache.Set(requestDTO.OperationType, schema);
                }
            }

            if (schema != null)
            {
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                JObject request = JObject.FromObject(requestDTO, new JsonSerializer
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = contractResolver
                });

                return request.IsValid(schema);
            }

            return false;
        }
    }
}
