using Azure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;
using PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Commands;
using PruebaTecnicaMultimoney.Core.Application.Services.Accounts.Queries;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;
using PruebaTecnicaMultimoney.Util;
using System.ClientModel.Primitives;

namespace PruebaTecnicaMultimoney.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : BaseApiController
    {

        [HttpGet("{accountId:}")]
        public async Task<IActionResult> GetAccountInfoAsync(string accountId)
        {

            var command = new GetAccountInfoQuery
            {
                AccountId = accountId
            };

            var result = await sender.Send(command);
            return ApiResponseMapper.MapResult(this, result, result.ReturnValue);

        }

        [HttpPost("{accountId:}/deposit")]
        public async Task<IActionResult> CreateDepositAsync(string accountId, [FromBody] CreateDepositRequest deposit)
        {

            var command = new CreateDepositCommand
            {
                AccountId = accountId,
                depositRequest = deposit
            };

            var result = await sender.Send(command);
            return ApiResponseMapper.MapResult(this, result, result.ReturnValue);

        }

        [HttpPost("{accountId:}/withdrawal")]
        public async Task<IActionResult> CreateWithdrawal(string accountId, [FromBody] CreateWithdrawalRequest withdrawal)
        {


            var command = new CreateWithdrawalCommand
            {
                AccountId = accountId,
                withdrawalRequest = withdrawal
            };

            var result = await sender.Send(command);
            return ApiResponseMapper.MapResult(this, result, result.ReturnValue);

        }
    }
}
