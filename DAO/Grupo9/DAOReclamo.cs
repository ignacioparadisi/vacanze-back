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
        /// Metodo para agregar un reclamo
        /// </summary>
        /// <param name="reclamo"></param>
        public void Agregar(Reclamo reclamo)
        {

            Conectar();

            StoredProcedure("AgregarReclamo(@rec_titulo,@rec_descripcion,@rec_status)");
            AgregarParametro("rec_titulo", reclamo.getTitulo());
            AgregarParametro("rec_descripcion", reclamo.getDescripcion());
            AgregarParametro("rec_status", reclamo.getStatus());

            EjecutarQuery();

        }

        /// <summary>
        /// Metodo para obtener un reclamo segun su id 
        /// </summary>
        /// <param name="numero"></param>
        /// que el logro no existe</exception>
        /// <returns></returns>
        public List<Reclamo> ObtenerReclamo(int numero)
        {
            List<Reclamo> ReclamoList = new List<Reclamo>();

            Conectar();
            StoredProcedure("ConsultarUnReclamo(@idReclamo)");
            AgregarParametro("idReclamo", numero);
            EjecutarReader();
            
            for (int i = 0; i < cantidadRegistros; i++)
            {
                Reclamo reclamo = new Reclamo();
                reclamo.setId(Convert.ToInt32(GetString(i, 0)));
                reclamo.setTitulo(GetString(i, 1));
                reclamo.setDescripcion(GetString(i, 2)) ;
                reclamo.setStatus(GetString(i, 3));

                //Console.WriteLine(reclamo.titulo+" " +reclamo.descripcion);               
                ReclamoList.Add(reclamo);
            }
            return ReclamoList;
        }
    }
}
 