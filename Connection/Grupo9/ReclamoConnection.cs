using System;
using System.Collections.Generic;
using vacanze_back.Entities.Grupo9;

namespace vacanze_back.Connection.Grupo9
{
    public class ReclamoConnection : Connection
    {
        public ReclamoConnection()
        {
            CreateStringConnection();
        }

        /// <summary>
        ///     Metodo para agregar un reclamo
        /// </summary>
        /// <param name="reclamo"></param>
        public void AgregarReclamo(Reclamo reclamo)
        {
            Connect();
            StoredProcedure("AgregarReclamo(@rec_titulo,@rec_descripcion,@rec_status)");
            AddParametro("rec_titulo", reclamo._titulo);
            AddParametro("rec_descripcion", reclamo._descripcion);
            AddParametro("rec_status", reclamo._status);

            EjecutarQuery();
        }

        /// <summary>
        ///     Metodo para obtener un reclamo segun su id
        /// </summary>
        /// <param name="numero"></param>
        /// que el logro no existe
        /// </exception>
        /// <returns></returns>
        public List<Reclamo> ObtenerReclamo(int numero)
        {
            var ReclamoList = new List<Reclamo>();

            Connect();
            StoredProcedure("ConsultarUnReclamo(@idReclamo)");
            AddParametro("idReclamo", numero);
            EjecutarReader();

            for (var i = 0; i < cantidadRegistros; i++)
            {
                var id = Convert.ToInt32(GetString(i, 0));
                var titulo = GetString(i, 1);
                var descripcion = GetString(i, 2);
                var status = GetString(i, 3);
                var reclamo = new Reclamo(id, titulo, descripcion, status);
                ReclamoList.Add(reclamo);
            }

            return ReclamoList;
        }

        /// <summary>
        ///     Metodo para elimar un reclamo con su id
        /// </summary>
        /// <param name="reclamoId"></param>
        public void EliminarReclamo(int reclamoId)
        {
            Connect();
            StoredProcedure("EliminarReclamo(@rec_id)");
            AddParametro("rec_id", reclamoId);
            EjecutarQuery();
        }

        public void ModificarReclamoStatus(long reclamoId, Reclamo reclamo)
        {
            Connect();
            StoredProcedure("ModificarReclamoStatus(@rec_id,@rec_status)");
            AddParametro("rec_id", reclamoId);
            AddParametro("rec_status", reclamo._status);
            EjecutarQuery();
        }

        public void ModificarReclamoTitulo(long reclamoId, Reclamo reclamo)
        {
            Connect();
            StoredProcedure("ModificarReclamoTitulo(@rec_id,@rec_titulo)");
            AddParametro("rec_id", reclamoId);
            AddParametro("rec_titulo", reclamo._titulo);
            EjecutarQuery();
        }
    }
}