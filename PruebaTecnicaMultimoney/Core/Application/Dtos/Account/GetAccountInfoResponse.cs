using PruebaTecnicaMultimoney.Core.Domain.Models;
using System.Text.Json.Serialization;

namespace PruebaTecnicaMultimoney.Core.Application.Dtos.Account
{
    public class GetAccountInfoResponse
    {
        [JsonIgnore]
        public int ReturnValue { get; set; }
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionResponse> Transactions { get; set; } = new();
    }
}
