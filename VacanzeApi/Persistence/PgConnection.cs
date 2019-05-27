using System;
using System.Data;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository
{
    public class PgConnection
    {
        private static PgConnection _instance;
        private readonly string _connectionParameters;

        private NpgsqlConnection _connection;

        private PgConnection(string host, string user, string password, string databaseName)
        {
            _connectionParameters =
                $"Server={host};User Id={user};Password={password};Database={databaseName}";
        }

        public static PgConnection Instance =>
            _instance ??
            (_instance = new PgConnection("192.168.99.100",
                "postgres",
                "docker",
                "postgres"));

        public DataTable ExecuteFunction(string functionSignature, params object[] arguments)
        {
            try
            {
                _connection = new NpgsqlConnection(_connectionParameters);
                _connection.Open();
                var command = new NpgsqlCommand("select * from " + functionSignature, _connection);
                if (arguments.Length > 0)
                {
                    var keys = ExtractParameters(functionSignature);
                    for (var i = 0; i < keys.Length; i++)
                        command.Parameters.AddWithValue(keys[i].Trim(), arguments[i]);
                }

                var dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());
                return dataTable;
            }
            catch (NpgsqlException)
            {
                throw new DatabaseException($"Error ejecutando funcion: {functionSignature}");
            }
            finally
            {
                _connection?.Close();
            }
        }

        private static string[] ExtractParameters(string procedureSignature)
        {
            try
            {
                var firstParenthesis = procedureSignature.IndexOf('(') + 1;
                var secondParenthesis = procedureSignature.IndexOf(')');
                var betweenParenthesis =
                    procedureSignature.Substring(firstParenthesis,
                        secondParenthesis - firstParenthesis);
                var keys = betweenParenthesis.Split(',');
                return keys;
            }
            catch (Exception e)
            {
                throw new InvalidStoredProcedureSignatureException(
                    $"Signature: {procedureSignature}");
            }
        }
    }
}