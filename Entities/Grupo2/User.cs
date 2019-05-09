namespace vacanze_back.Entities.Grupo2
{
    public class User: Entity
    {
        private long documentId;
        private string name;
        private string lastname;
        private string email;
        private string password;
        private Role role;

        public User(long id, long documentId, string name, string lastname, string email, string password, Role role)
        {
            setId(id);
            this.documentId = documentId;
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
            this.role = role;
        }

        public long getDocumentId()
        {
            return documentId;
        }

        public void setDocumentId(long documentId)
        {
            this.documentId = documentId;
        }
        
    }
}