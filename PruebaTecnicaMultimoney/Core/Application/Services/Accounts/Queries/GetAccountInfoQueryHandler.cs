using MediatR;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Queries
{
    public class GetAccountInfoQueryHandler : IRequestHandler<GetAccountInfoQuery, GetAccountInfoResponse>
    {
        private readonly IAccountRepository _accountsRepository;

        public GetAccountInfoQueryHandler(IAccountRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<GetAccountInfoResponse> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            string accountId = request.AccountId;
            return await _accountsRepository.GetAccountInfoAsync(accountId);
        }
    }
}
