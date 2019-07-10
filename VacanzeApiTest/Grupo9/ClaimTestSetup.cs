using System;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
    public class ClaimTestSetup
    {
        public static int BAGGAGE_ID;
        
        [OneTimeSetUp]
        public void Init()
        {
            // TODO: Fix
            var dt = PgConnection.Instance.ExecuteFunction(
                "addbaggage(@res_fli_fk, @res_cru_fk, @desc, @status)", 
                "null",
                "null",
                "ALGUNESTADO"
            );
            
            BAGGAGE_ID = Convert.ToInt32(dt.Rows[0][0]);
        }

        [OneTimeTearDown]
        public void End()
        {
            // LocationRepository.DeleteLocation(LOCATION_ID);
        }
    }
}