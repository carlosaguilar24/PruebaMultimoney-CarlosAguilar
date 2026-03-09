using MediatR;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Commands
{
    public class CreateWithdrawalCommand : IRequest<CreateWithdrawalResponse>
    {
        public string AccountId { get; set; }
        public CreateWithdrawalRequest withdrawalRequest { get; set; }
    }
}
