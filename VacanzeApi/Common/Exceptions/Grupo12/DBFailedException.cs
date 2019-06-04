using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo12
{
   public class DBFailedException : Exception{

    DBFailedException(){

    }

    public DBFailedException(string message):base(message){

    }

    public DBFailedException(string message,Exception exp):base(message,exp){

    }




    }
}