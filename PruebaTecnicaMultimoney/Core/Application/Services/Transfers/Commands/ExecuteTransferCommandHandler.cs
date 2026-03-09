using MediatR;
using Microsoft.Identity.Client;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Transfer;
using PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Commands;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Transfers.Commands
{
    public class ExecuteTransferCommandHandler : IRequestHandler<ExecuteTransferCommand, ExecuteTransferResponse>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ILogger<ExecuteTransferCommandHandler> _logger;

        public ExecuteTransferCommandHandler(ITransferRepository transferRepository, ILogger<ExecuteTransferCommandHandler> logger)
        {
            _transferRepository = transferRepository;
            _logger = logger;
        }
        public async Task<ExecuteTransferResponse> Handle(ExecuteTransferCommand request, CancellationToken cancellationToken)
        {
            var transferRequest = request.transferRequest;
            var result = await _transferRepository.ExecueTransfer(transferRequest);

            _logger.LogInformation(
           "Transfer {@Transfer} executed with status {ReturnValue}",
           new
           {
               OriginAccount = request.transferRequest.CuentaOrigen,
               DestinationAccount = request.transferRequest.CuentaDestino,
               Amount = request.transferRequest.Amount.ToString(),
               Description = request.transferRequest.Descripcion.ToString(),
           },
           result.ReturnValue
           );

            return result;
        }
    }
}
