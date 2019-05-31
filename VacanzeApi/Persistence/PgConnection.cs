using System;
using System.Data;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence
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

        // TODO: Obtener datos para conectar con la BD de algun archivo de configuracion
        public static PgConnection Instance =>
            _instance ??
            (_instance = new PgConnection("127.0.0.1",
                "postgres",
                "1234",
                "vacanza"));

        // <summary>
        // Ejecuta una funcion almacenada, pasando los argumentos recibidos
        // como parametros a la base de datos.
        // Devuelve:
        //    DataTable del resultado de la ejecucion de la funcion
        // Excepciones:
        //    - DatabaseException: Error conectandose a la base de datos, o ejecutando la funcion
        //    - InvalidStoredProcedureSignatureException: Cuando la firma de la funcion no puede ser
        //      procesada correctamente, esta excepcion no se deberia atrapar pues es generada por errores
        //      del programador.
        //
        // Ejemplos de uso correcto:
        //     ExecuteFunction("algunaFuncion(@param1, @param2, @param3)");
        //     ExecuteFunction("funcionSinParams()");
        //
        // Ejemplos de uso incorrecto:
        //     ExecuteFunction("algunaFuncion(param1, param2, param3)"); [No tiene los '@']
        //     ExecuteFunction("funcion mal escrita(();") [Espacios en el nombre de la funcion, parentesis de más]
        // </summary>
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
            catch (NpgsqlException e)
            {
                throw new DatabaseException(
                    $"Error ejecutando funcion: {functionSignature}.{Environment.NewLine}{e.Message}");
            }
            finally
            {
                _connection?.Close();
            }
        }

        // <summary>
        // Extrae los parametros que recibe un procedimiento almacenado segun la firma que
        // es recibida como parametro.
        // Por ejemplo:
        //     ExtractParameters("algunaFuncion(@param1, @param2, @param3)")
        //     Devolveria: ["@param1", "@param2", "@param3"]
        // </summary>
        private static string[] ExtractParameters(string procedureSignature)
        {
            try
            {
                // Conseguimos las posiciones de los parentesis que envuelven a los parametros
                var firstParenthesis = procedureSignature.IndexOf('(') + 1;
                var secondParenthesis = procedureSignature.IndexOf(')');
                // Extraemos el substring que contiene a los parametros
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