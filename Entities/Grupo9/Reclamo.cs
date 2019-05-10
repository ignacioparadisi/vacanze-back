namespace vacanze_back.Entities.Grupo9
{
    public class Reclamo: Entity
    {
        public long documentId;
        public string name;
        public string lastname;
        public string email;
        public string password;

        public Reclamo(long id, long documentId, string name, string lastname, string email, string password)
        {
            //setId(id);
            this.documentId = documentId;
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
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