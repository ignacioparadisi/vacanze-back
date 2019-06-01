using System;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class Claim : Entity
    {
        public string _title { get; set; }
        public string _description { get; set; }
        public string _status { get; set; }
        public int _idEquipaje { get; set; }
        
        public Claim(string title, string description, string status) : base(0)
        {
            _title = title;
            _description = description;
            _status = status;
        }

        public Claim(int id, string title, string description, string status) : base(id)
        {
            _title = title;
            _description = description;
            _status = status;
        }

        public Claim(int id, string title, string description, string status, int idEquipaje) : base(id)
        {
            _title = title;
            _description = description;
            _status = status;
            _idEquipaje= idEquipaje;
        }
        
        public Claim( string title, string description) : base(0)
        {
            _title = title;
            _description = description;
            _status = "ABIERTO";
        }

        public void Validate(){
            //excepcion para no permitir guardar otro valores que no sean el indicado
            if (_status != null)
            {
                if (_status != "ABIERTO" && _status != "CERRADO")
                {
                    throw new AttributeValueException("El atributo status no permite el valor ingresado");
                }
                
            }
            
            //excepciones por tamaño excedido permitido en base de datos
            if (_title != null)
            {
                if (_title.Length >40)
                {
                    throw new AttributeSizeException("El atributo titulo excede el tamaño permitido 40 caracteres");
                }
                
            }
            
        }

        public void ValidatePost()
        {
            if (_description == null | _title == null)
            {
                throw new AttributeValueException("Los atributos Descripcion y Titulo son obligatorios");
            }
            
        }

        public void ValidatePut()
        {
            if (_status != null)
            {
                if (_title != null ||_description != null)
                {
                    throw new AttributeValueException("Solo puede cambiar el status a la vez");
                }
            }
            else
            {
                if (_title != null && _description != null)
                {
                    if (_status != null )
                    {
                        throw new AttributeValueException("Solo puede cambiar titulo y descripcion a la vez, no puede mandar status");
                    }
                }
                else if (_title == null || _description == null)
                {
                    throw new AttributeValueException("le falta titulo o descripcion");
                }
                
            }
            
           
        }
    }
}    