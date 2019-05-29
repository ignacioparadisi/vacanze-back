using System;
using System.Collections.Generic;
using Npgsql;      
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo5
{
    public static class ConnectAuto 
    {

       public static int  Agregar(Auto auto)
        {
            
            String command ="ADDAUTOMOBILE (@AUT_MAKE,@AUT_MODEL,@AUT_CAPACITY,@AUT_ISACTIVE,@AUT_LICENSE,@AUT_PRICE,@AUT_PICTURE,@AUT_LOC_FK)";
            var table = PgConnection.Instance.ExecuteFunction(
                command,auto.getmake(),auto.getmodel(),auto.getcapacity(),auto.getisActive(), auto.getlicence(),auto.getprice(), auto.getpicture(),auto.getplace());
                
            var id = Convert.ToInt32(table.Rows[0][0]);
          return id;
       
        }
        
       public static int ModifyAuto (Auto auto)
        {
            string command = "MODIFYAUTOMOBILE (@AUT_ID,@AUT_MAKE,@AUT_MODEL,@AUT_CAPACITY,@AUT_ISACTIVE,@AUT_LICENSE,@AUT_PRICE,@AUT_PICTURE,@AUT_LOC_FK)";
            var table = PgConnection.Instance.ExecuteFunction(
                command,auto.getId(),auto.getmake(),auto.getmodel(),auto.getcapacity(),auto.getisActive(),auto.getlicence(),auto.getprice(), auto.getpicture()  ,auto.getplace());
            var id = Convert.ToInt32(table.Rows[0][0]);
            return id;
        }

       public static int DeleteAuto (int id){
           try{   
                string command="DeleteAuto (@AUT_ID)";
                var table = PgConnection.Instance.ExecuteFunction(command,id);
                return id;
            }catch (InvalidCastException){
                return -1;
            }catch (Exception ){
                return id;
            }
        }

       public static List<Auto> ConsultforId(int idconsulta){
            var AutoList = new List<Auto>();
            bool isactive;
            string command="consultforidauto (@AUT_ID)";
            var table = PgConnection.Instance.ExecuteFunction(command,idconsulta);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var make = table.Rows[i][1].ToString();
                var model = table.Rows[i][2].ToString();
                var capacity =Convert.ToInt32(table.Rows[i][3]);
                var status = table.Rows[i][4].ToString();
                var price = Convert.ToInt32(table.Rows[i][5]);
                var licence = table.Rows[i][6].ToString();
                var picture =table.Rows[i][7].ToString();
                var place =Convert.ToInt32(table.Rows[i][0]);
                if (status== "true"){
                    isactive=true;
                }else {isactive= false;}
                Auto auto=new Auto(make,model,capacity,isactive,licence,price,picture,place);
                auto.setId(id);
                AutoList.Add(auto);
            }
            return AutoList;
        }
    }
}