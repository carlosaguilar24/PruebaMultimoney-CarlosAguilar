using MediatR;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;

namespace PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Queries
{
    public class GetAccountInfoQuery : IRequest<GetAccountInfoResponse>
    {
        public string AccountId { get; set; }
    }
}
