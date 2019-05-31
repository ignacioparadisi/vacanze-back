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
              
            var resflight=(FlightRes) entity; 
            try{

                var capacity= PgConnection.Instance.ExecuteFunction("GetCapacityFlight(@id_flight)",id);
                int i= Convert.ToInt32(capacity.ToString()); 
                return i;
                /* 
                 PgConnection.Instance.ExecuteFunction(
                 "AddReservationFlight(@seatNum,@tim,@numPas,@id_user,@pay,@id_fli)",
                 resflight._seatNum,resflight._timestamp,resflight._numPas,
                 resflight._id_user,resflight._id_pay,resflight._id_fli);*/

            } catch(DBFailedException e){
            
                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException e){

                throw new DBFailedException("Tienes un error en el Stored Procedure",e);
            
            }
           
        }

        public List<FlightRes> GetReservationFlight(int id){
            var FlightList = new List<FlightRes>();
            var table = new DataTable();
           try{ 
                  
                table = PgConnection.Instance.ExecuteFunction("getReservationFlight(@rf_use_fk)",id);
                
                for (var i = 0; i < table.Rows.Count; i++){

                    int id_fli=Convert.ToInt32(table.Rows[i][0].ToString());
                    string seatNum=Convert.ToString(table.Rows[i][1].ToString());
                    string timeStam=Convert.ToString(table.Rows[i][2].ToString());
                    int numPas =Convert.ToInt32(table.Rows[i][3].ToString());
                    int rf_use=Convert.ToInt32(table.Rows[i][4].ToString());
                    int rf_pay=Convert.ToInt32(table.Rows[i][5].ToString());
                    int rf_fli=Convert.ToInt32(table.Rows[i][6].ToString());
                    var reservation_flight = new FlightRes(id_fli,seatNum,timeStam,numPas,rf_use,rf_pay,rf_fli);
                    FlightList.Add(reservation_flight);
                
                }
                return FlightList;

            }catch(DBFailedException e){

                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException e){

                throw new DBFailedException("Tienes un error en el Stored Procedure",e);
            
            }
            
        }

        public void DeleteReservationFlight(int id_reservation){
    
          try{
               
             PgConnection.Instance.ExecuteFunction("deletereservationflight(@id_reservation)",id_reservation);
             
            }catch(DBFailedException e){

                throw new DBFailedException("Tienes un error en la base de datos",e);
            
            }catch(InvalidStoredProcedureSignatureException e){

                throw new DBFailedException("Tienes un error en el Stored Procedure",e);
            
            }
            
        }





    }
}