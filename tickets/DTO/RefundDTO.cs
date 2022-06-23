namespace tickets.DTO
{
    public class RefundDTO : IRequestDTO
    {
        public string OperationType { get; set; }

        public string OperationTime { get; set; }

        public string OperationPlace { get; set; }

        public string TicketNumber { get; set; }
    }
}
