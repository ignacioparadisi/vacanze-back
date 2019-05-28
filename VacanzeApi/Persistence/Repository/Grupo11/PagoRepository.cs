using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo11;
using vacanze_back.VacanzeApi.Common.Util;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo11
{
    public class PagoRepository
    {


        public static List<Order> GetInfoOrder(long id,int tipo)
        {
            var table = PgConnection.Instance.ExecuteFunction("Get_Info_Order(@id,@tipo)", id,tipo);
            return ConvertDt.ConvertDataTable<Order>(table);
        }




    }
}
