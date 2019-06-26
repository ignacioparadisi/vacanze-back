using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo9
{
    public class Claim : Entity
    {
        public Claim(string title, string description, string status) : base(0)
        {
            Title = title;
            Description = description;
            Status = status;
        }

        public Claim(int id, string title, string description, string status) : base(id)
        {
            Title = title;
            Description = description;
            Status = status;
        }

        public Claim(int id, string title, string description, string status, int baggageId) : base(id)
        {
            Title = title;
            Description = description;
            Status = status;
            BaggageId = baggageId;
        }

        public Claim(string title, string description) : base(0)
        {
            Title = title;
            Description = description;
            Status = "ABIERTO";
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int BaggageId { get; set; }

        // TODO: Extraer estas validaciones a una clase externa, y utilizar excepciones RequiredAttributeException etc

        public void Validate()
        {
            //excepcion para no permitir guardar otro valores que no sean el indicado
            if (Status != null)
                if (Status != "ABIERTO" && Status != "CERRADO")
                    throw new AttributeValueException("El atributo status no permite el valor ingresado");

            //excepciones por tamaño excedido permitido en base de datos
            if (Title != null)
                if (Title.Length > 40)
                    throw new AttributeSizeException("El atributo titulo excede el tamaño permitido 40 caracteres");
        }

        public void ValidatePost()
        {
            if ((Description == null) | (Title == null))
                throw new AttributeValueException("Los atributos Descripcion y Titulo son obligatorios");
        }

        public void ValidatePut()
        {
            if (Status != null)
            {
                if (Title != null || Description != null)
                    throw new AttributeValueException("Solo puede cambiar el status a la vez");
            }
            else
            {
                if (Title != null && Description != null)
                {
                    if (Status != null)
                        throw new AttributeValueException(
                            "Solo puede cambiar titulo y descripcion a la vez, no puede mandar status");
                }
                else if (Title == null || Description == null)
                {
                    throw new AttributeValueException("le falta titulo o descripcion");
                }
            }
        }
    }
}