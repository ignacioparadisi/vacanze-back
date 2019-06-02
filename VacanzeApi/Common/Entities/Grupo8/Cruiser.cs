
using Newtonsoft.Json;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class Cruiser
    {
            public int Id { get; set; }
            public string Name { get; }
            public bool Status { get; }
            public int Capacity { get; }
            public int LoadingShipCap { get; }
            public string Model { get; }
            public string Line { get; }
            public string Picture { get; }

            [JsonConstructor]
            public Cruiser( int id , string name , bool status , int capacity , int loadingShipCap , string model , string line,string picture)
            {
                Id = id;
                Name = name;
                Status = status;
                Capacity = capacity;
                LoadingShipCap = loadingShipCap;
                Model = model;
                Line = line;
                Picture = picture;
            }
            public Cruiser(string name , bool status , int capacity , int loadingShipCap , string model , string line, string picture)
            {
                Name = name;
                Status = status;
                Capacity = capacity;
                LoadingShipCap = loadingShipCap;
                Model = model;
                Line = line;
                Picture = picture;
            }

            public void Validate()
            {
                if (string.IsNullOrEmpty(Name))
                {
                    throw new InvalidAttributeException("El nombre es requerido");
                }
                if (Capacity <= 0)
                {
                    throw new InvalidAttributeException("La capacidad tiene que ser mayor a 0");
                }
                if (LoadingShipCap <= 0)
                {
                    throw new InvalidAttributeException("La capacidad de carga debe ser mayor a 0");
                }
                if (string.IsNullOrEmpty(Model))
                {
                    throw  new InvalidAttributeException("El modelo es requerido");
                }
                if (string.IsNullOrEmpty(Line))
                {
                    throw new InvalidAttributeException("La Linea del crucero es requerida");
                }

                if (string.IsNullOrEmpty(Picture))
                {
                    throw new InvalidAttributeException("La imagen es requerida");
                }
            }
        }
}