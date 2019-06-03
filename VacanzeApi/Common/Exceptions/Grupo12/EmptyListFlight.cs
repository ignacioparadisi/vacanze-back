using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo12
{
   public class EmptyListFlight : Exception{

    EmptyListFlight(){

    }

    public EmptyListFlight(string message):base(message){

    }

    public EmptyListFlight(string message,Exception exp):base(message,exp){

    }




    }
}