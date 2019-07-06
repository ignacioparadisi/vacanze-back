using System.Net.Http;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9
{
    public class BaggageValidator
    {
        public static void Validate(Baggage baggage, HttpMethod method = null)
        {
            
            if (method == HttpMethod.Put && baggage.Status != null)
            {
                if (baggage.Description != null) throw new AttributeValueException("Solo puede cambiar el status");
            }
            
            // No permitir guardar otros valores que no sean el indicado
            if (baggage.Status != null && baggage.Status != "EXTRAVIADO" && baggage.Status != "ACTIVO" && baggage.Status != "RECLAMADO" && baggage.Status != "ENCONTRADO")
                throw new AttributeValueException("El atributo \"Status\" no permite el valor ingresado");
            
        }
    }
}