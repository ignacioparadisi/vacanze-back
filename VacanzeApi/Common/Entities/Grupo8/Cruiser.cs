using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class Cruiser : Entity
    {
            public string Name { get; }
            public bool Status { get; }
            public int Capacity { get; }
            public int LoadingShipCap { get; }
            public string Model { get; }
            public string Line { get; }

            [JsonConstructor]
            public Cruiser( long id , string name , bool status , int capacity , int loadingShipCap , string model , string line) : base(id)
            {
                Name = name;
                Status = status;
                Capacity = capacity;
                LoadingShipCap = loadingShipCap;
                Model = model;
                Line = line;
            }
            public Cruiser(string name , bool status , int capacity , int loadingShipCap , string model , string line) : base(0)
            {
                Name = name;
                Status = status;
                Capacity = capacity;
                LoadingShipCap = loadingShipCap;
                Model = model;
                Line = line;
            }
        }
}