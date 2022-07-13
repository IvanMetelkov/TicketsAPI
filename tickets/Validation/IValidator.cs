using System.Threading.Tasks;
using tickets.DTO;

namespace tickets.Validation
{
    public interface IValidator
    {
        public Task<bool> ValidateJsonAsync(string jsonString, string operation);
    }
}