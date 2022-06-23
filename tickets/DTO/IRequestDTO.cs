using Newtonsoft.Json;
using System;

namespace tickets.DTO
{
    public interface IRequestDTO
    {
        public string OperationType { get; set; }

        public string OperationTime { get; set; }

        public string OperationPlace { get; set; }
    }
}
