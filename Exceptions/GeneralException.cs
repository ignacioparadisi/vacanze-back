using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vacanze_back.Exceptions
{
    public class GeneralException :Exception
    {
        private Exception _exception;   //Tipo de excepcion que se genero.
        private DateTime _date;   //Hora y fecha de cuando se genero la excepción.
        private string _menssage;       //Breve descripción de la excepción genereda.

        public GeneralException ( Exception excepcion, DateTime fechaHora)
        {
            _exception = excepcion;
            _date = fechaHora;
            _menssage = "Error general";
        }

        /// <summary>
        /// Getters y Setters del atributo _excepcion
        /// </summary>
        public Exception exception { get => _exception; set => _exception = value; }
        
        /// <summary>
        /// Getters y Setters del atributo _fechaHora
        /// </summary>
        public DateTime Date { get => _date; set => _date = value; }
        
        /// <summary>
        /// Getters y Setters del atributo _mensaje
        /// </summary>
        public string Menssage { get => _menssage; set => _menssage = value; }
    }
}