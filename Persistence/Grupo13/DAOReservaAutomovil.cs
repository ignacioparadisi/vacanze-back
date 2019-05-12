using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.Entities;
using vacanze_back.Entities.Grupo13;
using vacanze_back.Persistence;

namespace vacanze_back.Persistence.Grupo13
{
    public class DAOReservationAutomobile:DAO
    {
        const String SP_SELECT = "{CALL m13_getreservaautomoviles()}";
        /*
                private Automovil automovil;

                public List<Entity> getAutomoviles()
                {
                    List<Entity> automovilList = new List<Entity>();
                    try
                    {
                        if (Connect())
                        {
                            StoredProcedure(SP_SELECT);
                            ExecuteReader();
                            for (int i = 0; i < rowNumber; i++)
                            {
                                Automovil automovil = new Automovil();
                                //(hab_id integer, hab_precio float, hab_capacidad integer, hab_tipo character varying,fk_hot_id integer)
                                // id, estatus, capacidad, precio, fk_hotel. 
                                automovil.setId(GetInt(i, 0));
                                automovil.precio = GetDouble(i, 1);
                                automovil.capacidad = GetInt(i, 2);
                                automovil.estatus = GetBool(i, 3);

                                automovilList.Add(automovil);
                            }
                        }
                    }
                    catch (NpgsqlException e)
                    {
                        e.ToString();
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }
                    finally
                    {
                        Disconnect();
                    }
                    return habitacionList;
                }*/
    }

}
