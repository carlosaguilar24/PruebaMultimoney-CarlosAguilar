using MediatR;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Transfer;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Transfers.Commands
{
    public class ExecuteTransferCommand : IRequest<ExecuteTransferResponse>
    {
        public ExecuteTransferRequest transferRequest {  get; set; }
    }
}
