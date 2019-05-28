
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.Entities.Grupo11
{
    public class Payment :Entity
    {
       

        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; private set; }
     

        public Payment(int _id , string _name, bool _active ) : base(_id)
        {
            id = _id;
            name = _name;
            active =_active;
        }


        public long getId()
        {
            return id;
        }

        public void setId(int _id)
        {
            id = _id;
        }


        public string getName()
        {
            return name;
        }

        public void setName(string _name)
        {
            name = _name;
        }


        public bool getActive()
        {
            return active;
        }

        public void setActive(bool _active)
        {
            active = _active;
        }
    }
}


