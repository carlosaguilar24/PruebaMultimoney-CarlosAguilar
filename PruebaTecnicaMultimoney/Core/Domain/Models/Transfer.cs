namespace PruebaTecnicaMultimoney.Core.Domain.Models
{
    public class Transfer
    {
        public string OirignOriginAccount { get; set; }
        public string DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
