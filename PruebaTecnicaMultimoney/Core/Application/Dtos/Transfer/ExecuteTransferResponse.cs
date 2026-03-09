using System.Text.Json.Serialization;

namespace PruebaTecnicaMultimoney.Core.Application.Dtos.Transfer
{
    public class ExecuteTransferResponse
    {
        [JsonIgnore]
        public int ReturnValue { get; set; }
        public TransactionsTransferDtoResponse Origin { get; set; }
        public TransactionsTransferDtoResponse Destiny { get; set; }
    }
}
