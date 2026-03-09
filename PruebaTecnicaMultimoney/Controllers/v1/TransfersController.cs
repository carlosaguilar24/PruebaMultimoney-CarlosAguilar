using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Transfer;
using PruebaTecnicaMultimoney.Core.Application.Services.Transfers.Commands;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;
using PruebaTecnicaMultimoney.Util;

namespace PruebaTecnicaMultimoney.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransfersController : BaseApiController
    {
        
        [HttpPost]
        public async Task<IActionResult> ExecuteTransfer(ExecuteTransferRequest transfer)
        {
            var command = new ExecuteTransferCommand
            {
                transferRequest = transfer
            };

            var result = await sender.Send(command);
            return ApiResponseMapper.MapResult(this, result, result.ReturnValue);
        }
    }
}
