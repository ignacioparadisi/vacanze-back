using System.Net.Http;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.LogicLayer.Command;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9
{
    public class ClaimValidator
    {
        public static void Validate(Claim claim, HttpMethod method = null)
        {
            if (method == HttpMethod.Put)
            {
                if (claim.Status != null)
                {
                    if (claim.Title != null || claim.Description != null)
                        throw new AttributeValueException("Solo puede cambiar el status a la vez");
                }
                else
                {
                    if (claim.Title != null && claim.Description != null && claim.Status != null)
                        throw new AttributeValueException(
                            "Solo puede cambiar titulo y descripcion a la vez, no puede mandar status");

                    if (claim.Title == null || claim.Description == null)
                        throw new AttributeValueException("Falta titulo o descripcion");
                }
            }
            else if (method == HttpMethod.Post)
            {
                if (claim.Description == null)
                    throw new RequiredAttributeException("Atributo \"Description\" es obligatorio");

                if (claim.Title == null)
                    throw new RequiredAttributeException("Atributo \"Title\" es obligatorio");

                if (claim.BaggageId == 0)
                    throw new RequiredAttributeException("Atributo \"BaggageId\" es obligatorio y diferente a 0");

                // Si no se consigue la maleta lanzara un BaggageNotFoundException
                CommandFactory.CreateGetBaggageByIdCommand(claim.BaggageId).Execute();
            }

            // No permitir guardar otros valores que no sean el indicado
            if (claim.Status != null && claim.Status != "ABIERTO" && claim.Status != "CERRADO")
                throw new AttributeValueException("El atributo \"Status\" no permite el valor ingresado");

            // Validar que el titulo no sea mas largo que la capacidad en la base de datos
            if (claim.Title != null && claim.Title.Length > 40)
                throw new AttributeSizeException("El atributo \"Title\" excede el tamaño permitido 40 caracteres");
        }
    }
}