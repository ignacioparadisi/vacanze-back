using System;
using System.Collections.Generic;
using Npgsql;      
using vacanze_back.Entities.Grupo9;
 

namespace vacanze_back.DAO.Grupo9
{
    public class DAOReclamo: DAO
    {

        public DAOReclamo()
        {
            CrearStringConexion();
        }

        /// <summary>
        /// Metodo para agregar un logro por equipo
        /// </summary>
        /// <param name="entidad"></param>
        public void Agregar(Reclamo reclamo)
        {

            Conectar();

            StoredProcedure("AgregarReclamo(@rec_titulo,@rec_descripcion,@rec_status)");
            AgregarParametro("rec_titulo", reclamo.titulo);
            AgregarParametro("rec_descripcion", reclamo.descripcion);
            AgregarParametro("rec_status", reclamo.status);

            EjecutarQuery();

        }

        /// <summary>
        /// Metodo para obtener logro su id 
        /// </summary>
        /// <param name="entidad"></param>
        /// <exception cref="LogroNoExisteException">Excepcion que indica 
        /// que el logro no existe</exception>
        /// <returns></returns>
        public List<Reclamo> ObtenerReclamo(int numero)
        {
            List<Reclamo> ReclamoList = new List<Reclamo>();
            Reclamo reclamo = new Reclamo();

            Conectar();
            StoredProcedure("ConsultarUnReclamo(@idReclamo)");
            AgregarParametro("idReclamo", numero);
            EjecutarReader();
            
            for (int i = 0; i < cantidadRegistros; i++)
            {
                reclamo.titulo = GetString(i, 1);
                reclamo.descripcion = GetString(i, 2);
                reclamo.status = GetString(i, 3);

                Console.WriteLine(reclamo.titulo+" " +reclamo.descripcion);               
                ReclamoList.Add(reclamo);
            }
            return ReclamoList;
        }
    }
}
 