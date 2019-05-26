using System;
using System.Data;
using Npgsql;

namespace vacanze_back.Persistence.Connection
{
    public abstract class Connection
    {
        private NpgsqlConnection _con;
        private NpgsqlCommand _command;
        private DataTable _dataTable;
        private string _cadena;
        private int _numberRecords;

        public Connection()
        {
            CreateStringConnection();
        }

        public int numberRecords
        {
            get { return _numberRecords; }
        }

        /// <summary>
        /// hay que optimizar la cadena porque hasta ahora la hacemos local
        /// </summary>
        protected void CreateStringConnection()
        {
			_cadena = "Server=127.0.0.1;User Id=vacanza;" + 
                        "Password=vacanza;Database=vacanza;" ;
            //cadena de prueba
            /*
			_cadena = "Server=127.0.0.1;User Id=postgres;" + 
                        "Password=jorge;Database=postgres;" ;*/
        }

        private bool IsConnected()
        {
            if (_con == null)
                return false;

            if (_con.State == System.Data.ConnectionState.Open)
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

        public void disconnect()
        {
            if (_con != null && IsConnected())
                _con.Close();
        }

        /// <summary>
        /// Ejecutar el StoredProcedure con un valor de retorno (ResultSet), habilita el uso de las funciones "GetInt, GetString, etc" y devuelve un objeto DataTable.
        /// </summary>
        public DataTable ExecuteReader()
        {

            try
            {

                _dataTable = new DataTable();

                _dataTable.Load(_command.ExecuteReader());

                disconnect();

                _numberRecords = _dataTable.Rows.Count;

            }
            catch (NpgsqlException exc)
            {
                disconnect();
                throw exc;
            }
            catch (Exception exc)
            {
                disconnect();
                throw exc;
            }

            return _dataTable;

        }


        /// <summary>
        /// Ejecutar el StoredProcedure sin valor de retorno (ResultSet), devuelve el número de filas afectadas.
        /// </summary>
        public int ExecuteQuery()
        {
            try
            {
                int filasAfectadas = _command.ExecuteNonQuery();

                disconnect();

                return filasAfectadas;
            }
            catch (NpgsqlException exc)
            {
				Console.WriteLine(exc);
                disconnect();
                throw exc;
            }
            catch (Exception exc) 
            {
                disconnect();
                throw exc;
            }
        }

        /// <summary>
        /// Crea el comando para ejecutar el StoredProcedure, Ejemplo: StoredProcedure("nombreSP(@param)")
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
                int intItem = Convert.ToInt32(_dataTable.Rows[fila][columna]);

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
                char charItem = Convert.ToChar(_dataTable.Rows[fila][columna]);

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
                string stringItem = Convert.ToString(_dataTable.Rows[fila][columna]);

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
                double doubleItem = Convert.ToDouble(_dataTable.Rows[fila][columna]);

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
                decimal decimalItem = Convert.ToDecimal(_dataTable.Rows[fila][columna]);

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
                bool boolItem = Convert.ToBoolean(_dataTable.Rows[fila][columna]);

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
                DateTime dateItem = Convert.ToDateTime(_dataTable.Rows[fila][columna]);

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
				byte[] dateItem = (byte[])_dataTable.Rows[fila][columna];

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
