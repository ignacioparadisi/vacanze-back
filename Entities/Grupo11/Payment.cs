using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vacanze_back.Entities.Gripo11
{
    public class Payment :Entity
    {
       

        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        //public bool _active { get; private set; }

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

        public void setActive(string _active)
        {
            active = _active;
        }
    }
}



/**
 *     public class User : Entity
    {
        private long _documentId;
        private string _email;
        private string _lastname;
        private string _name;
        private string _password;
        private Role _role;

        public User(long id, long documentId, string name, string lastname, string email,
            string password, Role role) : base(id)
        {
            _documentId = documentId;
            _name = name;
            _lastname = lastname;
            _email = email;
            _password = password;
            _role = role;
        }

        public long getDocumentId()
        {
            return _documentId;
        }

        public void setDocumentId(long documentId)
        {
            _documentId = documentId;
        }
    }

    */




