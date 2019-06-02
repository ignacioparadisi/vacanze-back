using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo2
{
    public class Role
    {
        
        public const int CLIENT = 1;
        public const int ADMIN = 2;
        public const int CHECKIN = 2;
        public const int CLAIM = 2;
        public const int CARRIER = 2;
        public string Name { get; set; }
        public int Id { get; set; }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Validate()
        {
            if (Id <= 0)
            {
                throw new NotValidIdException("El " + Id + " rol es inválido");
            }
        }
        
        
    }
}