using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NLog.Targets.Wrappers;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class Cruiser : Entity
    {
            [Required]
            public string Name { get; }
            [Required]
            public bool Status { get; }
            [Required]
            public int Capacity { get; }
            [Required]
            public int LoadingShipCap { get; }
            [Required]
            public string Model { get; }
            [Required]
            public string Line { get; }
            [Required]
            public string Picture { get; }

            [JsonConstructor]
            public Cruiser( long id , string name , bool status , int capacity , int loadingShipCap , string model , string line,string picture) : base(id)
            {
                Name = name;
                Status = status;
                Capacity = capacity;
                LoadingShipCap = loadingShipCap;
                Model = model;
                Line = line;
                Picture = picture;
            }
            public Cruiser(string name , bool status , int capacity , int loadingShipCap , string model , string line, string picture) : base(0)
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
                if (Capacity <= 0)
                {
                    throw new NotValidAttributeException("La capacidad tiene que ser mayor a 0");
                }
                if (LoadingShipCap <= 0)
                {
                    throw new NotValidAttributeException("La capacidad de carga debe ser mayor a 0");
                }
            }
        }
}