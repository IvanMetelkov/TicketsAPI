using System.Threading.Tasks;
using tickets.DTO;

namespace tickets.DAL
{
    public interface IValidator
    {
        public Task<bool> ValidateDTOAsync(IRequestDTO request);
    }
}
