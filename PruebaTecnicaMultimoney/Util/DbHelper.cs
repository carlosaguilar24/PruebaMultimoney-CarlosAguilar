using Microsoft.Data.SqlClient;
using System.Data;

namespace PruebaTecnicaMultimoney.Util
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ExecuteReaderAsync(
           string storedProcedure,
           Action<SqlCommand> parameterBuilder,
           Func<SqlDataReader, Task> readerHandler)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(storedProcedure, connection);

            command.CommandType = CommandType.StoredProcedure;

            parameterBuilder(command);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            await readerHandler(reader);
        }

        public async Task<int> ExecuteNonQueryAsync(
        string storedProcedure,
        Action<SqlCommand> parameterBuilder)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(storedProcedure, connection);

            command.CommandType = CommandType.StoredProcedure;

            var returnValue = new SqlParameter
            {
                Direction = ParameterDirection.ReturnValue
            };

            command.Parameters.Add(returnValue);

            parameterBuilder(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (int)returnValue.Value;
        }
    }
}
