namespace vacanze_back.Entities.Grupo9
{
    public class Reclamo: Entity
    {
        private long _id;
        private string _titulo;
        private string _descripcion;
        private string _status;

        public Reclamo(string titulo, string descripcion, string status)
        {
            this._id= 0;
            this._titulo = titulo;
            this._descripcion = descripcion;
            this._status = status;
        }
        
        public Reclamo()
        {
        }
        public long getId()
        {
            return _id;
        }

        public void seId(long documentId)
        {
            this._id = documentId;
        }
        public string getTitulo()
        {
            return _titulo;
        }

        public void setTitulo(string titulo)
        {
            this._titulo = titulo;
        }
        public string getDescripcion()
        {
            return _descripcion;
        }

        public void setDescripcion(string descripcion)
        {
            this._descripcion = descripcion;
        }
        public string getStatus()
        {
            return _status;
        }
        public void setStatus(string status)
        {
            this._status = status;
        }
 
        
        
    }
}