namespace vacanze_back.Common.Entities.Grupo2
{
    public class Role : Entity
    {
        public string Name { get; set; }

        public Role(long id, string name) : base(id)
        {
            this.Name = name;
        }
        
        
    }
}