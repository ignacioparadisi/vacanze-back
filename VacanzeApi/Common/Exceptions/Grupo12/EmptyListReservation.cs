using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo12
{
   public class EmptyListReservation : Exception{

    EmptyListReservation(){

    }

    public EmptyListReservation(string message):base(message){

    }

    public EmptyListReservation(string message,Exception exp):base(message,exp){

    }




    }
}