using System.Text.Json.Serialization;

namespace PruebaTecnicaMultimoney.Core.Application.Dtos.Account
{
    public class CreateDepositResponse
    {
        [JsonIgnore]
        public int ReturnValue { get; set; }
        public decimal NewBalance { get; set; }
    }
}
