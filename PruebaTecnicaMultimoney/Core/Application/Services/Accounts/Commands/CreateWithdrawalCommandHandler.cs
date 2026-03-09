using Azure;
using MediatR;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Commands
{
    public class CreateWithdrawalCommandHandler : IRequestHandler<CreateWithdrawalCommand, CreateWithdrawalResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<CreateWithdrawalCommandHandler> _logger;

        public CreateWithdrawalCommandHandler(IAccountRepository accountRepository, ILogger<CreateWithdrawalCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<CreateWithdrawalResponse> Handle(CreateWithdrawalCommand request, CancellationToken cancellationToken)
        {
            string accountId = request.AccountId;
            var withdrawalRequest = request.withdrawalRequest;

            var result = await _accountRepository.CreateWithdrawalAsync(accountId, withdrawalRequest);

            _logger.LogInformation(
            "Withdrawal {@Withdrawal} executed with status {ReturnValue}",
            new
            {
                AccountId = accountId,
                Balance = result.NewBalance
            },
            result.ReturnValue
            );

            return result;
        }
    }
}
