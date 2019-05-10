namespace vacanze_back.Entities.Grupo9
{
    public class Reclamo: Entity
    {
        private long documentId;
        private string name;
        private string lastname;
        private string email;
        private string password;

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