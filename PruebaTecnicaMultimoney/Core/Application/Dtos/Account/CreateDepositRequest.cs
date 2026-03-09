namespace PruebaTecnicaMultimoney.Core.Application.Dtos.Account
{
    public class CreateDepositRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
