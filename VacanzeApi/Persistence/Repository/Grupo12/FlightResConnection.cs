using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo12
{
    public class  FlightResConnection 
    {

        public int AddReservationFlight(Entity entity){
            var table = new DataTable();
            var resflight=(FlightRes) entity; 
            try{
                table=PgConnection.Instance.ExecuteFunction(
                "AddReservationFlight(@seatNum,@tim,@numPas,@id_user,@id_fli)",
                resflight._seatNum,resflight._timestamp,resflight._numPas,
                resflight._id_user,resflight._id_fli);
                resflight._id_user=Convert.ToInt32(table.Rows[0][0].ToString());
                return resflight._id_user;
            } catch(DBFailedException e){
            
                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }
           
        }

        public List<FlightRes> GetReservationFlight(int id){
            var FlightList = new List<FlightRes>();
            var table = new DataTable();
           try{ 
                  
                table = PgConnection.Instance.ExecuteFunction("getReservationFlight(@rf_use_fk)",id);
                
                for (var i = 0; i < table.Rows.Count; i++){

                    int id_fli = Convert.ToInt32(table.Rows[i][0].ToString());
                    string seatNum = Convert.ToString(table.Rows[i][1].ToString());
                    string timeStam = Convert.ToString(table.Rows[i][2].ToString());
                    int numPas = Convert.ToInt32(table.Rows[i][3].ToString());
                    int rf_use = Convert.ToInt32(table.Rows[i][4].ToString());
                    int rf_pay = Convert.ToInt32(table.Rows[i][5].ToString());
                    int rf_fli = Convert.ToInt32(table.Rows[i][6].ToString());
                    var reservation_flight = new FlightRes(id_fli,seatNum,timeStam,numPas,rf_use,rf_pay,rf_fli);
                    FlightList.Add(reservation_flight);
                
                }
                return FlightList;

            }catch(DBFailedException e){

                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }
            
        }

        public void DeleteReservationFlight(int id_reservation){
    
          try{
               
            var i=PgConnection.Instance.ExecuteFunction
            ("deletereservationflight(@id_reservation)",id_reservation);
            Console.WriteLine("--------------->",i);
            }catch(DBFailedException e){

                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException){

                throw new InvalidStoredProcedureSignatureException("Tienes un error en el Stored Procedure");
            
            }
            
        }

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


        public List<ListRes> GetFlightValidate(int id_city_i,int id_city_v,string date_i,int numpas){
            int cont=0;
            var ListRes = new List<ListRes>();
            var listres=new ListRes();
            var table1= new DataTable();
            var table2= new DataTable();
            var table3= new DataTable();
            var table4= new DataTable();
            var table5= new DataTable();
            try{
                
                table1 = PgConnection.Instance.ExecuteFunction("GetFlightsIDA(@_departure,@_arrival,@_departuredate)"
                ,id_city_i,id_city_v,date_i);

               

                for(var i = 0; i < table1.Rows.Count; i++){

                    table4 = PgConnection.Instance.ExecuteFunction("GetNameLocation(@_id_city)"
                    ,Convert.ToInt32(table1.Rows[i][4]));

                    table5= PgConnection.Instance.ExecuteFunction("GetNameLocation(@_id_city)"
                    ,Convert.ToInt32(table1.Rows[i][5]));
                    
                    table2 = PgConnection.Instance.ExecuteFunction("getSum(@id_flight)",
                    Convert.ToInt32(table1.Rows[i][0].ToString()));

                    table3 = PgConnection.Instance.ExecuteFunction("getCapacity(@idfligh)",
                    Convert.ToInt32(table1.Rows[i][0].ToString()));
                    
                    listres._sum_pas=Convert.ToInt32(table2.Rows[0][0].ToString());
                    listres._sum_capacity= Convert.ToInt32(table3.Rows[0][0].ToString());
                    
                    
                    cont=listres._sum_capacity-listres._sum_pas;
                   
                     if(cont>=numpas){
                         
                        listres._id=Convert.ToInt32(table1.Rows[i][0].ToString());
                        listres._price=Convert.ToInt32(table1.Rows[i][1].ToString());
                        listres._priceupdate=Convert.ToInt32(table1.Rows[i][1].ToString())*numpas;
                        listres._dateI=table1.Rows[i][2].ToString();
                        listres._seatavailable=cont;
                        listres._name_country_i=table4.Rows[0][2].ToString();
                        listres._name_country_V=table5.Rows[0][2].ToString();
                         
                         var listreservationflight = new ListRes( listres._id,listres._price,
                        listres._priceupdate,listres._dateI,listres._name_country_i,
                        listres._name_country_V,listres._seatavailable);
                         
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