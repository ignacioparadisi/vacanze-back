// using System;
// using System.Collections.Generic;
// using vacanze_back.Entities.Grupo9;

// namespace vacanze_back.DAO.Grupo9
// {
//     public class DAOReclamo : DAO
//     {
//         public DAOReclamo()
//         {
//             CrearStringConexion();
//         }

//         /// <summary>
//         ///     Metodo para agregar un reclamo
//         /// </summary>
//         /// <param name="reclamo"></param>
//         public void AgregarReclamo(Reclamo reclamo)
//         {
//             Conectar();
//             StoredProcedure("AgregarReclamo(@rec_titulo,@rec_descripcion,@rec_status)");
//             AgregarParametro("rec_titulo", reclamo._titulo);
//             AgregarParametro("rec_descripcion", reclamo._descripcion);
//             AgregarParametro("rec_status", reclamo._status);

//             EjecutarQuery();
//         }

//         /// <summary>
//         ///     Metodo para obtener un reclamo segun su id
//         /// </summary>
//         /// <param name="numero"></param>
//         /// que el logro no existe
//         /// </exception>
//         /// <returns></returns>
//         public List<Reclamo> ObtenerReclamo(int numero)
//         {
//             var ReclamoList = new List<Reclamo>();

//             Conectar();
//             StoredProcedure("ConsultarUnReclamo(@idReclamo)");
//             AgregarParametro("idReclamo", numero);
//             EjecutarReader();

//             for (var i = 0; i < cantidadRegistros; i++)
//             {
//                 var id = Convert.ToInt32(GetString(i, 0));
//                 var titulo = GetString(i, 1);
//                 var descripcion = GetString(i, 2);
//                 var status = GetString(i, 3);
//                 var reclamo = new Reclamo(id, titulo, descripcion, status);
//                 ReclamoList.Add(reclamo);
//             }

//             return ReclamoList;
//         }

//         /// <summary>
//         ///     Metodo para elimar un reclamo con su id
//         /// </summary>
//         /// <param name="reclamoId"></param>
//         public void EliminarReclamo(int reclamoId)
//         {
//             Conectar();
//             StoredProcedure("EliminarReclamo(@rec_id)");
//             AgregarParametro("rec_id", reclamoId);
//             EjecutarQuery();
//         }

//         public void ModificarReclamoStatus(long reclamoId, Reclamo reclamo)
//         {
//             Conectar();
//             StoredProcedure("ModificarReclamoStatus(@rec_id,@rec_status)");
//             AgregarParametro("rec_id", reclamoId);
//             AgregarParametro("rec_status", reclamo._status);
//             EjecutarQuery();
//         }

//         public void ModificarReclamoTitulo(long reclamoId, Reclamo reclamo)
//         {
//             Conectar();
//             StoredProcedure("ModificarReclamoTitulo(@rec_id,@rec_titulo)");
//             AgregarParametro("rec_id", reclamoId);
//             AgregarParametro("rec_titulo", reclamo._titulo);
//             EjecutarQuery();
//         }
//     }
// }