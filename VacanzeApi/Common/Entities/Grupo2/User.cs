namespace vacanze_back.VacanzeApi.Common.Entities.Grupo2
{
    public class User : Entity
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
}