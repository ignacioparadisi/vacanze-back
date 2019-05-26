using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace vacanze_back.Entities.Grupo8
{
    [JsonObject(MemberSerialization.OptIn)] 
    public class Cruiser : Entity
    {
        [JsonProperty] private string name;
        [JsonProperty] private int roms;
        [JsonProperty] private int capacity;

        public Cruiser
            (string name, int roms, int capacity )
        {
            this.name = name;
            this.roms = roms;
            this.capacity = capacity;
        }
    }
}