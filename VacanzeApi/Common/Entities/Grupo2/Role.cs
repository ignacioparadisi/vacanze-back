namespace vacanze_back.Common.Entities.Grupo2
{
    public class Role : Entity
    {
        private string name;

        public Role(long id, string name) : base(id)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }
    }
}