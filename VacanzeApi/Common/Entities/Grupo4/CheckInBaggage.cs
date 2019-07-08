namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
     public class CheckinBaggage : Entity
     {
        public CheckinBaggage(int id, string _descrip, int _fli, int _cru) : base(id)
        {
            descrip = _descrip;
            fli = _fli;
            cru = _cru;
        }
        public CheckinBaggage(): base(0)
        {

        }
        public string descrip { get; set; }
        public int fli { get; set; }
        public int cru { get; set; }

     }
}

