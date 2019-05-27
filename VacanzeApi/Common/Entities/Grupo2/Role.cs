using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo2
{
    public class Role : Entity
    {
        public string Name { get; set; }

        public Role(long id, string name) : base(id)
        {
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