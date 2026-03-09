using MediatR;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Commands
{
    public class CreateDepositCommand : IRequest<CreateDepositResponse>
    {

        public string AccountId { get; set; }
        public CreateDepositRequest depositRequest { get; set; }
    }
}
