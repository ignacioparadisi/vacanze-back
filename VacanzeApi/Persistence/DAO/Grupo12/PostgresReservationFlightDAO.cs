using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo12
{
    public class PostgresReservationFlightDAO : ReservationFlightDAO
    {

        private const string ADD_RES_FLIGHT = "AddReservationFlight(@seatNum,@tim,@numPas,@id_user,@id_fli)";
        private const string GET_RES_FLIGHT = "getReservationFlight(@rf_use_fk)";
        private const string GET_NAME_LOCATION = "GetNameLocation(@_id_city)";
        
        /// <param name="Entity">Es on objeto de tipo FightRes </param>
        /// <returns>Devuelve el id que se agrego</returns>
        public int AddReservationFlight(Entity entity){
            var flight = new DataTable();
            var resflight=(FlightRes) entity; 
            try{
                
                flight=PgConnection.Instance.ExecuteFunction(ADD_RES_FLIGHT,
                resflight._seatNum,resflight._timestamp,resflight._numPas,
                resflight._id_user,resflight._id_fli);
                resflight._idres=Convert.ToInt32(flight.Rows[0][0].ToString());

                if(resflight._idres.Equals(0)){

                    throw new EmptyReservationException("Ha ocurrido un error agregando la reserva");

                }

                return resflight._id_user;
            
            } catch(DBFailedException e){
            
                throw new DBFailedException("Ha ocurrido un en la Base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Ha ocurrido un en la Base de datos");
            
            }
           
        }



        /// <param name="id">id del usuario </param>
        /// <returns>Devuelve la lista de reservas de un usuario</returns>
        public List<Entity> GetReservationFlight(int id){
            var FlightRes = new List<Entity>();
            var table = new DataTable();
            var res=new FlightRes();
            var reservation_flight=new FlightRes();
           try{ 
                  
                table = PgConnection.Instance.ExecuteFunction(GET_RES_FLIGHT,id);
                var table2 = new DataTable();
                
                for (int i = 0; i < table.Rows.Count; i++){
                    
                    table2 = PgConnection.Instance.ExecuteFunction(GET_NAME_LOCATION,
                    Convert.ToInt32(table.Rows[i][6].ToString()));

                    res._id=Convert.ToInt32(table.Rows[i][0]);
                    res._price=Convert.ToInt32(table.Rows[i][1]);
                    res._timestamp=Convert.ToString(table.Rows[i][2].ToString());
                    res._seatNum=Convert.ToString(table.Rows[i][3].ToString());
                    res._namecountryI=table.Rows[i][4].ToString();
                    res._namecityI=table.Rows[i][5].ToString();
                    res._numPas=Convert.ToInt32(table.Rows[i][7].ToString());
                    res._namecityV=table2.Rows[0][1].ToString();
                    res._namecountryV=table2.Rows[0][2].ToString();
               
                  
                    reservation_flight = new FlightRes(res._id,res._price,
                    res._timestamp,res._seatNum, res._namecityI,res._namecountryI,
                    res._namecityV,res._namecountryV,res._numPas);



                    FlightRes.Add(reservation_flight);
                }

                if(FlightRes.Count.Equals(0)){
                     throw new EmptyListReservation("Disculpe no se encontraron reservas");
                }
                return FlightRes;

            }catch(DBFailedException e){

                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }
            
        }



        /// <param name="id_reservation">id de la reservacion </param>
        /// <returns>Elimina la reserva que se especifico con el id</returns>
        public void DeleteReservationFlight(int id_reservation){
    
          try{
               
            var i=PgConnection.Instance.ExecuteFunction
            ("deletereservationflight(@id_reservation)",id_reservation);
          
            }catch(DBFailedException e){

                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }
            
        }

        
        /// <param name="name_city">Nombre de la ciudad</param>
        /// <returns>Devuelve el id de la ciudad</returns>
        public int GetIDLocation(string name_city){
            var res=new FlightRes();
            var table=new DataTable();
            try{
                table=PgConnection.Instance.ExecuteFunction("GetIDLocation(@name_city)",name_city);
                res._id_city=Convert.ToInt32(table.Rows[0][0].ToString());
                return res._id_city;
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }
            
        }


        /// <param name="id_city_i">Id de la ciudad de origen</param>
        /// <param name="id_city_v">Id de la ciudad de destino</param>
        /// <param name="date_i">Fecha de ida de una reserva</param>
        /// <param name="numpas">Cantidad de pasajeros</param>
        /// <returns>Devuelve la lista de reservas validado (IDA)</returns>        
        public List<Entity> GetFlightValidateI(int id_city_i,int id_city_v,string date_i,int numpas){
            int cont=0;
            var ListRes = new List<Entity>();
            var listres=new ListRes();
            var table1= new DataTable();
            var table2= new DataTable();
            var table3= new DataTable();
            var table4= new DataTable();
            var table5= new DataTable();
            try{
                
                table1 = PgConnection.Instance.ExecuteFunction("GetFlightsIDA(@_departure,@_arrival,@_departuredate)"
                ,id_city_i,id_city_v,date_i);
                listres._price=table1.Rows.Count;
               
               if(listres._price.Equals("0")){
                   throw new EmptyListFlight("Disculpe no se Encontraron Vuelos Disponibles para esa Fecha");
               }

                for(int i = 0; i < table1.Rows.Count; i++){

                    table4 = PgConnection.Instance.ExecuteFunction(GET_NAME_LOCATION
                    ,Convert.ToInt32(table1.Rows[i][4]));

                    table5= PgConnection.Instance.ExecuteFunction(GET_NAME_LOCATION
                    ,Convert.ToInt32(table1.Rows[i][5]));
                    
                    table2 = PgConnection.Instance.ExecuteFunction("getSum(@id_flight)",
                    Convert.ToInt32(table1.Rows[i][0].ToString()));

                    table3 = PgConnection.Instance.ExecuteFunction("getCapacity(@idfligh)",
                    Convert.ToInt32(table1.Rows[i][0].ToString()));
                    
                    listres._sum_pas=Convert.ToInt32(table2.Rows[0][0].ToString());
                    listres._sum_capacity= Convert.ToInt32(table3.Rows[0][0].ToString());
                    
                    cont=listres._sum_capacity-listres._sum_pas;
                   
                     if(cont>=numpas){
                         Console.WriteLine(cont);
                        listres._id=Convert.ToInt32(table1.Rows[i][0].ToString());
                        listres._price=Convert.ToInt32(table1.Rows[i][1].ToString());
                        listres._priceupdate=Convert.ToInt32(table1.Rows[i][1].ToString())*numpas;
                        listres._dateI=table1.Rows[i][2].ToString();
                        listres._seatavailable=cont;
                        listres._name_country_i=table4.Rows[0][2].ToString();
                        listres._name_country_V=table5.Rows[0][2].ToString();
                        listres._name_city_i=table4.Rows[0][1].ToString();
                        listres._name_city_V=table5.Rows[0][1].ToString();
                         
                         var listreservationflight = new ListRes( listres._id,listres._price,
                        listres._priceupdate,listres._dateI,listres._name_country_i,
                        listres._name_country_V,listres._seatavailable,listres._name_city_i,
                        listres._name_city_V);
                         
                        ListRes.Add(listreservationflight);
                        cont=0;
                        
                    }
    
                }
 
               return ListRes;

            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            }
        }


        /// <param name="numseat">Cantidad de asiento</param>
        /// <param name="_id_fli">Id del vuelo</param>
        /// <returns>Devuelve los numeros de asientos de una reserva</returns>
        public string ConSeatNum(int numseat,int _id_fli){
            var table1= new DataTable();
            var res=new FlightRes();
            try{
                
                table1=PgConnection.Instance.ExecuteFunction("getSum(@idflight)",_id_fli);
                res._sum_pas=Convert.ToInt32(table1.Rows[0][0].ToString());
                 Console.WriteLine(res._sum_pas);
                int cont=res._sum_pas;
                string seat="";
                if(numseat!=0){
                    for (int i = 0; i < numseat; i++){
                        cont=cont+1;
                        seat+="A-"+cont+",";
                    }
                }else{
                    return "0";
                }
                return seat;    
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }
        }



        /// <param name="departure">Id de la ciudad de origen</param>
        /// <param name="arrival">Id de la ciudad de destino</param>
        /// <param name="departuredate">Fecha de origen</param>
        /// <param name="arrivaldate">Fecha de destino</param>
        /// <param name="numpas">Cantidad de pasajeros</param>
        /// <returns>Devuelve la lista de reservacion de validado (IDA y VUELTA)</returns>
        public List<Entity> GetReservationFlightIV(int departure, int arrival, string departuredate,string arrivaldate,int numpas){
            int cont=0;
            var ListRes = new List<Entity>();
            var listres=new ListRes();
            var table1= new DataTable();
            var table2= new DataTable();
            var table3= new DataTable();
            var table4= new DataTable();
            var table5= new DataTable();
            try{
                
                table1 = PgConnection.Instance.ExecuteFunction("GetFlightsIDAVU(@_departure,@_arrival,@_departuredate,@arrivaldate)"
                ,departure,arrival,departuredate,arrivaldate);
                listres._price=table1.Rows.Count;
               
               if(listres._price.Equals("0")){
                   throw new EmptyListFlight("Disculpe no se Encontraron Vuelos Disponibles para esa Fecha");
               }
               
                for(int i = 0; i < table1.Rows.Count; i++){
                    table4 = PgConnection.Instance.ExecuteFunction(GET_NAME_LOCATION
                    ,Convert.ToInt32(table1.Rows[i][5]));
                    table5= PgConnection.Instance.ExecuteFunction(GET_NAME_LOCATION
                    ,Convert.ToInt32(table1.Rows[i][6]));
                    table2 = PgConnection.Instance.ExecuteFunction("getSum(@id_flight)",
                    Convert.ToInt32(table1.Rows[i][0].ToString()));
                    table3 = PgConnection.Instance.ExecuteFunction("getCapacity(@idfligh)",
                    Convert.ToInt32(table1.Rows[i][0].ToString()));
                    listres._sum_pas=Convert.ToInt32(table2.Rows[0][0].ToString());
                    listres._sum_capacity= Convert.ToInt32(table3.Rows[0][0].ToString());
                   
                    cont=listres._sum_capacity-listres._sum_pas;
                   
                     if(cont>=numpas){
                        Console.WriteLine(cont);
                        listres._id=Convert.ToInt32(table1.Rows[i][0].ToString());
                        listres._price=Convert.ToInt32(table1.Rows[i][2].ToString());
                        listres._priceupdate=Convert.ToInt32(table1.Rows[i][2].ToString())*numpas;
                        listres._dateI=table1.Rows[i][3].ToString();
                        listres._dateV=table1.Rows[i][4].ToString();
                        listres._seatavailable=cont;
                        listres._name_country_i=table4.Rows[0][2].ToString();
                        listres._name_country_V=table5.Rows[0][2].ToString();
                        listres._name_city_i=table4.Rows[0][1].ToString();
                        listres._name_city_V=table5.Rows[0][1].ToString();
                         
                        var listreservationflight = new ListRes( listres._id,listres._price,
                        listres._priceupdate,listres._dateI,listres._dateV,listres._name_country_i,
                        listres._name_country_V,listres._seatavailable,listres._name_city_i,
                        listres._name_city_V);
                         
                        ListRes.Add(listreservationflight);
                        cont=0;
                        
                    }
    
                }
 
               return ListRes;

            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }

        }
    }
}