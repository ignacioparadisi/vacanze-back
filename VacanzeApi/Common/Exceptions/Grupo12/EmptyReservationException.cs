using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions.Grupo12
{
   public class EmptyReservationException : Exception{

    EmptyReservationException(){

    }

    public EmptyReservationException(string message):base(message){

    }

    public EmptyReservationException(string message,Exception exp):base(message,exp){

    }




    }
}