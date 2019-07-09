using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo4;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo4
{
    public class PostgresCheckBaggagetDAO : ICheckinBaggageDAO
    {

        private const string SP_POSTFLIGHTSALE = "PostCheckBaggage(@_descrip,@_fli,@_cru)";


        public int PostCheckBaggage(List<CheckinBaggage> postCheck)
        {
            int _ireturn = 0;
            try
            {
                if (postCheck.Count > 0)
                {
                    postCheck.ForEach(det => {

                        var resultTable = PgConnection.Instance.ExecuteFunction(SP_POSTFLIGHTSALE,det.descrip,det.fli,det.cru);
                    });


                }
                else
                {
                    throw new ErrorCheckBaggageException();
                }

            }
            catch (Exception ex)
            {

                throw new ErrorCheckBaggageException();
            }
            return _ireturn;
        }
    }
}
    
