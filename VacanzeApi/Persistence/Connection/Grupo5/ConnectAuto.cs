using System;
using System.Collections.Generic;
using Npgsql;      
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo5
{
    public class ConnectAuto : Connection
    {
        public ConnectAuto()
        {
            CreateStringConnection();
        }

        public void Agregar(Auto auto)
        {
            Connect();
            StoredProcedure("ADDAUTOMOBILE (@AUT_MAKE,@AUT_MODEL,@AUT_CAPACITY,@AUT_ISACTIVE,@AUT_LICENSE,@AUT_PRICE,@AUT_PICTURE,@AUT_LOC_FK)");
            AddParameter("AUT_MAKE", auto.getmake().ToString());
            AddParameter("AUT_MODEL", auto.getmodel().ToString());
            AddParameter("AUT_CAPACITY", auto.getcapacity());
            AddParameter("AUT_ISACTIVE", auto.getisActive());
            AddParameter("AUT_LICENSE", auto.getlicence().ToString());
            AddParameter("AUT_PRICE", auto.getprice());
            AddParameter("AUT_PICTURE", auto.getpicture());
            AddParameter("AUT_LOC_FK", auto.getplace());          

            ExecuteQuery();
        }
        public void ModifyAuto (Auto auto)
        {
            Connect();
            StoredProcedure("MODIFYAUTOMOBILE (@AUT_ID,@AUT_MAKE,@AUT_MODEL,@AUT_CAPACITY,@AUT_ISACTIVE,@AUT_LICENSE,@AUT_PRICE,@AUT_PICTURE,@AUT_LOC_FK)");
            AddParameter("AUT_ID",auto.getId());
            AddParameter("AUT_MAKE", auto.getmake().ToString());
            AddParameter("AUT_MODEL", auto.getmodel().ToString());
            AddParameter("AUT_CAPACITY", auto.getcapacity());
            AddParameter("AUT_ISACTIVE", auto.getisActive());
            AddParameter("AUT_LICENSE", auto.getlicence().ToString());
            AddParameter("AUT_PRICE", auto.getprice());
            AddParameter("AUT_PICTURE", auto.getpicture());
            AddParameter("AUT_LOC_FK", auto.getplace());          

            ExecuteQuery();
        }
        public void DeleteAuto ( int id){
            Connect();
            StoredProcedure("DeleteAuto (@AUT_ID)");
            AddParameter("AUT_ID", id);
            ExecuteQuery();
        }

        public List<Auto> ConsultforId(int id){
            var AutoList = new List<Auto>();
            Connect();
            StoredProcedure("consultforidauto (@AUT_ID)");
            AddParameter("AUT_ID", id);
            ExecuteReader();
            Console.WriteLine("la madre " + numberRecords);
            for (var i = 0; i < numberRecords; i++)
                {
                     Console.WriteLine("entro");
                    var ids = Convert.ToInt32(GetString(i, 0));
                    var make = GetString(i, 1);
                    var model = GetString(i, 2);
                    var capacity =Convert.ToInt32(GetString(i, 3));
                    var status = GetBool(i, 4);
                    var price =Convert.ToInt32(GetString(i, 5));
                    var licence =GetString(i, 6);
                    var picture =GetString(i, 7);
                    var place =Convert.ToInt32(GetString(i, 3));
                    Auto auto=new Auto(make,model,capacity,status,licence,price,picture,place);
                    auto.setId(ids);
                    AutoList.Add(auto);
                
                }
            return AutoList;
        }
    }
}