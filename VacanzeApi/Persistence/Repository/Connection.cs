using System;
using System.Data;
using Npgsql;

namespace vacanze_back.VacanzeApi.Persistence.Repository
{
    public abstract class Connection
    {
        private string _cadena;
        private NpgsqlCommand _command;
        private NpgsqlConnection _con;
        private DataTable _dataTable;

        public Connection()
        {
            CreateStringConnection();
        }

        public int numberRecords { get; private set; }

        /// <summary>
        ///     hay que optimizar la cadena porque hasta ahora la hacemos local
        /// </summary>
        protected void CreateStringConnection()
        {
            _cadena = "Server=192.168.99.100;User Id=postgres;" +
                      "Password=docker;Database=postgres;";
            //cadena de prueba
            /*
			_cadena = "Server=127.0.0.1;User Id=postgres;" + 
                        "Password=jorge;Database=postgres;" ;*/
        }

        private bool IsConnected()
        {
            if (_con == null)
                return false;

            if (_con.State == ConnectionState.Open)
                return true;

            return false;
        }

        public bool Connect()
        {
            try
            {
                _con = new NpgsqlConnection(_cadena);
                _con.Open();
                return true;
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Disconnect()
        {
            if (_con != null && IsConnected())
                _con.Close();
        }

        /// <summary>
        ///     Ejecutar el StoredProcedure con un valor de retorno (ResultSet), habilita el uso de las
        ///     funciones "GetInt, GetString, etc" y devuelve un objeto DataTable.
        /// </summary>
        public DataTable ExecuteReader()
        {
            try
            {
                _dataTable = new DataTable();

                _dataTable.Load(_command.ExecuteReader());

                Disconnect();

                numberRecords = _dataTable.Rows.Count;
            }
            catch (NpgsqlException exc)
            {
                Disconnect();
                throw exc;
            }
            catch (Exception exc)
            {
                Disconnect();
                throw exc;
            }

            return _dataTable;
        }


        /// <summary>
        ///     Ejecutar el StoredProcedure sin valor de retorno (ResultSet), devuelve el número de filas
        ///     afectadas.
        /// </summary>
        public int ExecuteQuery()
        {
            try
            {
                var filasAfectadas = _command.ExecuteNonQuery();

                Disconnect();

                return filasAfectadas;
            }
            catch (NpgsqlException exc)
            {
                Console.WriteLine(exc);
                Disconnect();
                throw exc;
            }
            catch (Exception exc)
            {
                Disconnect();
                throw exc;
            }
        }

        /// <summary>
        ///     Crea el comando para ejecutar el StoredProcedure, Ejemplo: StoredProcedure("nombreSP(@param)")
        /// </summary>
        public NpgsqlCommand StoredProcedure(string sp)
        {
            try
            {
                _command = new NpgsqlCommand("select * from " + sp, _con);
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            return _command;
        }


        public void AddParameter(string nombre, object valor)
        {
            try
            {
                _command.Parameters.AddWithValue("@" + nombre, valor);
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            catch (NullReferenceException exc)
            {
                throw exc;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public int GetInt(int fila, int columna)
        {
            try
            {
                var intItem = Convert.ToInt32(_dataTable.Rows[fila][columna]);

                return intItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (OverflowException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public char GetChar(int fila, int columna)
        {
            try
            {
                var charItem = Convert.ToChar(_dataTable.Rows[fila][columna]);

                return charItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetString(int fila, int columna)
        {
            try
            {
                var stringItem = Convert.ToString(_dataTable.Rows[fila][columna]);

                return stringItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public double GetDouble(int fila, int columna)
        {
            try
            {
                var doubleItem = Convert.ToDouble(_dataTable.Rows[fila][columna]);

                return doubleItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (OverflowException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public decimal GetDecimal(int fila, int columna)
        {
            try
            {
                var decimalItem = Convert.ToDecimal(_dataTable.Rows[fila][columna]);

                return decimalItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (OverflowException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool GetBool(int fila, int columna)
        {
            try
            {
                var boolItem = Convert.ToBoolean(_dataTable.Rows[fila][columna]);

                return boolItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DateTime GetDateTime(int fila, int columna)
        {
            try
            {
                var dateItem = Convert.ToDateTime(_dataTable.Rows[fila][columna]);

                return dateItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public byte[] GetByte(int fila, int columna)
        {
            try
            {
                var dateItem = (byte[]) _dataTable.Rows[fila][columna];

                return dateItem;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}