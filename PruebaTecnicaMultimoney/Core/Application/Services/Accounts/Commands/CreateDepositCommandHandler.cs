using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;
using PruebaTecnicaMultimoney.Infrastructure.Repositories;
using System.Data;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Commands
{
    public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, CreateDepositResponse>
    {
        private readonly IAccountRepository _accountsRepository;
        private readonly ILogger<CreateDepositCommandHandler> _logger;


        public CreateDepositCommandHandler(IAccountRepository accountsRepository, ILogger<CreateDepositCommandHandler> logger)
        {
            _accountsRepository = accountsRepository;
            _logger = logger;
        }

        public async Task<CreateDepositResponse> Handle(CreateDepositCommand request, CancellationToken cancellationToken)
        {
            string accountId = request.AccountId;
            var depositRequest = request.depositRequest;

            var response = await _accountsRepository.CreateDepositAsync(accountId, depositRequest);

            _logger.LogInformation(
              "Deposit {@Deposit} executed with status {ReturnValue}",
              new
              {
                AccountId = accountId,
                Balance = response.NewBalance
              },
              response.ReturnValue
              );

            return response;
        }
    }
}
