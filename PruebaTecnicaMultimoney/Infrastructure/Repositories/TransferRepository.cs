using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Transfer;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;
using PruebaTecnicaMultimoney.Core.Domain.Models;
using PruebaTecnicaMultimoney.Util;
using System.Data;

namespace PruebaTecnicaMultimoney.Infrastructure.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly DbHelper _dbHelper;
        private readonly ILogger<TransferRepository> _logger;


        public TransferRepository(DbHelper dbHelper, ILogger<TransferRepository> logger)
        {
            _dbHelper = dbHelper;
            _logger = logger;
        }
        public async Task<ExecuteTransferResponse> ExecueTransfer(ExecuteTransferRequest request)
        {
            _logger.LogInformation("Creating transfer from account {AccountOrigin} to {AccountDestination}", request.CuentaOrigen, request.CuentaDestino);


            var trasnferResponse = new ExecuteTransferResponse();
            var returnValue = new SqlParameter
            {
                Direction = ParameterDirection.ReturnValue
            };

            _logger.LogInformation("Executing sp_ExecuteTransfer from account {AccountOrigin} to {AccountDestination}", request.CuentaOrigen, request.CuentaDestino);


            await _dbHelper.ExecuteReaderAsync(
                "sp_ExecuteTransfer",
                command =>
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@OriginAccount", SqlDbType.NVarChar).Value = request.CuentaOrigen;
                    command.Parameters.Add("@DestinationAccount", SqlDbType.NVarChar).Value = request.CuentaDestino;
                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = request.Amount;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = request.Descripcion;
                    command.Parameters.Add(returnValue);
                }, async reader =>
                {
                    if (await reader.ReadAsync())
                    {
                        trasnferResponse.Origin = new TransactionsTransferDtoResponse
                        {
                            TransactionId = reader.GetInt32(0),
                            AccountId = reader.GetString(1),
                            Type = reader.GetString(2),
                            Amount = reader.GetDecimal(3),
                            date = reader.GetDateTime(4),
                            Description = reader.GetString(5)

                        };
                    }

                    await reader.NextResultAsync();

                    if (await reader.ReadAsync())
                    {
                        trasnferResponse.Destiny = new TransactionsTransferDtoResponse
                        {
                            TransactionId = reader.GetInt32(0),
                            AccountId = reader.GetString(1),
                            Type = reader.GetString(2),
                            Amount = reader.GetDecimal(3),
                            date = reader.GetDateTime(4),
                            Description = reader.GetString(5)

                        };
                    }
                });

            _logger.LogInformation("Execution completed sp_ExecuteTransfer from account {AccountOrigin} to {AccountDestination}", request.CuentaOrigen, request.CuentaDestino);

            trasnferResponse.ReturnValue = (int)returnValue.Value;
            return trasnferResponse;

        }
    }
}
