using PruebaTecnicaMultimoney.Core.Application.Dtos.Transfer;

namespace PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories
{
    public interface ITransferRepository
    {
        Task<ExecuteTransferResponse> ExecueTransfer(ExecuteTransferRequest transferResponse);
    }
}
