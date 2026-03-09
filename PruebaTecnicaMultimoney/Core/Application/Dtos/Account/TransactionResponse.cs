namespace PruebaTecnicaMultimoney.Core.Application.Dtos.Account
{
    public class TransactionResponse
    {
        public int TransactionId { get; set; }
        public string AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; }
    }
}
