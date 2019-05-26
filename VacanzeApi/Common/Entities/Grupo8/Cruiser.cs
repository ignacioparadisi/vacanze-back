namespace vacanze_back.VacanzeApi.Common.Entities.Grupo8
{
    public class Cruiser : Entity
    {
            private string Name { get; }
            private int Roms { get; }
            private int Capacity { get; }

            public Cruiser(long id,string name, int roms, int capacity ) : base(id)
            {
                Name = name;
                Roms = roms;
                Capacity = capacity;
            }
        }
}