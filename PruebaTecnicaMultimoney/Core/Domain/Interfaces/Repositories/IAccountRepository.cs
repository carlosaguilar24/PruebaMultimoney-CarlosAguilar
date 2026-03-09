using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;

namespace PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<GetAccountInfoResponse> GetAccountInfoAsync(string  accountId);
        Task<CreateDepositResponse> CreateDepositAsync(string accountId, CreateDepositRequest depositRequest);
        Task<CreateWithdrawalResponse> CreateWithdrawalAsync(string accountId, CreateWithdrawalRequest withdrawalRequest);

    }
}
