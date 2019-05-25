namespace vacanze_back.Common.Entities.Grupo2
{
    public class Role : Entity
    {
        private string name { get; set; }

        public Role(long id, string name) : base(id)
        {
            this.name = name;
        }
    }
}