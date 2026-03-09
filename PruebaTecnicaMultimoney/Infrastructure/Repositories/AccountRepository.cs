using Microsoft.Data.SqlClient;
using PruebaTecnicaMultimoney.Core.Application.Dtos.Account;
using PruebaTecnicaMultimoney.Core.Domain.Interfaces.Repositories;
using PruebaTecnicaMultimoney.Core.Domain.Models;
using PruebaTecnicaMultimoney.Util;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace PruebaTecnicaMultimoney.Infrastructure.Repositories
{
    public class AccountsRepository : IAccountRepository
    {
        private readonly DbHelper _dbHelper;
        private readonly ILogger<AccountsRepository> _logger;

        public AccountsRepository(DbHelper dbHelper, ILogger<AccountsRepository> logger )
        {
            _dbHelper = dbHelper;
            _logger = logger;
        }
        public async Task<GetAccountInfoResponse> GetAccountInfoAsync(string accountId)
        {
            _logger.LogInformation("Executing sp_GetAccountInfo for {AccountId}",accountId);

            var accountInfoResponse = new GetAccountInfoResponse();
            await _dbHelper.ExecuteReaderAsync(
                "sp_GetAccountInfo",
                command => command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = accountId,
                async reader =>
                {

                    if (await reader.ReadAsync())
                    {
                        accountInfoResponse.AccountId = reader.GetString(0);
                        accountInfoResponse.Balance = reader.GetDecimal(1);
                    }

                    await reader.NextResultAsync();

                    var transactions = new List<TransactionResponse>();

                    while (await reader.ReadAsync())
                    {
                        transactions.Add(new TransactionResponse()
                        {
                            TransactionId = reader.GetInt32(0),
                            Type = reader.GetString(1),
                            Amount = reader.GetDecimal(2),
                            date = reader.GetDateTime(3),
                            Description = reader.GetString(4)
                        });
                    }

                    accountInfoResponse.Transactions = transactions;
                }
                );

            _logger.LogInformation("Information completed for {AccountId}.",accountId);
            return accountInfoResponse;
        }

        public async Task<CreateDepositResponse> CreateDepositAsync(string accountId, CreateDepositRequest depositRequest)
        {

            _logger.LogInformation("Creating deposit for account {AccountId} with amount {Amount}",
            accountId, depositRequest.Amount);

            var newBalanceParam = new SqlParameter("@NewBalance", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };

            _logger.LogInformation("Executing sp_GetAccountInfo for account {AccountId}",accountId);

            var result = await _dbHelper.ExecuteNonQueryAsync(
                "sp_CreateDeposit",
                command =>
                {
                    command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = accountId;
                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = depositRequest.Amount;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = depositRequest.Description;
                    command.Parameters.Add(newBalanceParam);
                });

            _logger.LogInformation("Execution completed sp_GetAccountInfo for account {AccountId}", accountId);

            return new CreateDepositResponse
            {
                ReturnValue = result,
                NewBalance = result == 0 ? (decimal)newBalanceParam.Value : 0

            };

        }

        public async Task<CreateWithdrawalResponse> CreateWithdrawalAsync(string accountId, CreateWithdrawalRequest withdrawalRequest)
        {

            _logger.LogInformation("Creating withdrawal for account {Account}", accountId);


            var newBalanceParam = new SqlParameter("@NewBalance", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };

            _logger.LogInformation("Executing sp_CreateWithdrawal for account {AccountId}", accountId);

            var result = await _dbHelper.ExecuteNonQueryAsync(
                "sp_CreateWithdrawal",
                command =>
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = accountId;
                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = withdrawalRequest.Amount;
                    command.Parameters.Add(newBalanceParam);
                });

            _logger.LogInformation("Execution completed sp_CreateWithdrawal for account {AccountId}", accountId);

            return new CreateWithdrawalResponse
            {
                ReturnValue = result,
                NewBalance = result == 0 ? (decimal)newBalanceParam.Value : 0
            };

        }

       
    }
}
