namespace vacanze_back.Entities.Grupo9
{
    public class Reclamo : Entity
    {
        public Reclamo(long id, string titulo, string descripcion, string status) : base(id)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Status = status;
        }

        public Reclamo(string titulo, string descripcion, string status) : base(0)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Status = status;
        }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Status { get; set; }
    }
}