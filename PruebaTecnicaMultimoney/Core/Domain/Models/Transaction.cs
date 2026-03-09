namespace PruebaTecnicaMultimoney.Core.Domain.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string AccountId { get; set; }
        public int TransactionTypeId {  get; set; }
        public decimal Amount { get; set; }
        public DateTime date { get; set; }
        public string Description { get; set; }
    }
}
