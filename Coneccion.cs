using System;
 using Npgsql;      
class Coneccion
{
   private  NpgsqlConnection conn;
     public void conectarBd()
    {
        // Specify connection options and open an connection
         conn = new NpgsqlConnection(
            "Server=127.0.0.1;User Id=postgres;" + 
            "Password=jorge;Database=postgres;"
        );
        conn.Open();
    }
    public void ejecutarQuery(string query){
        // Define a query
        NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

        // Execute a query
        NpgsqlDataReader dr = cmd.ExecuteReader();

        // Read all rows and output the first column in each row
        while (dr.Read())
        Console.WriteLine("{0}\n", dr[0]);
        
        
        // Close connection
        conn.Close();
    }
        

}
