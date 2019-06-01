

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class EmailRepository
    {
        public String Recovery(string Email)
        {
            try
            {
                var view = PgConnection.Instance.ExecuteFunction("RecoveryPass(@Email)",Email);
                var Password = view.Rows[0][2].ToString();
                return Password;
            }
            catch (Exception e)
            {
                var m = e.Message;
                throw new Exception("Emaiil does not exist on our BDD");
            }

        }
    }
}
