namespace PruebaTecnicaMultimoney.Core.Application.Dtos.Transfer
{
    public class ExecuteTransferRequest
    {
        public string CuentaOrigen { get; set; }
        public string CuentaDestino { get; set; }
        public decimal Amount { get; set; }
        public string Descripcion { get; set; }
    }
}
