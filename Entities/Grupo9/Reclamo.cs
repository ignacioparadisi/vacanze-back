namespace vacanze_back.Entities.Grupo9
{
    public class Reclamo: Entity
    {
        public long documentId;
        public string titulo;
        public string lastname;
        public string descripcion;
        public string status;

        public Reclamo(string titulo, string descripcion, string status)
        {
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.status = status;
        }
        
        public Reclamo()
        {
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