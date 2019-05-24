namespace vacanze_back.Entities.Grupo9
{
    public class Reclamo : Entity
    {
        public string _titulo { get; set; }
        public string _descripcion { get; set; }
        public string _status { get; set; }
        
        public Reclamo(string titulo, string descripcion, string status) : base(0)
        {
            _titulo = titulo;
            _descripcion = descripcion;
            _status = status;
        }

        public Reclamo(long id, string titulo, string descripcion, string status) : base(id)
        {
            _titulo = titulo;
            _descripcion = descripcion;
            _status = status;
        }
    }
}