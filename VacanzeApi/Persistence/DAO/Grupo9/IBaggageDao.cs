using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo9
{
    public interface IBaggageDao
    {
        Baggage GetById(int id);

        List<Baggage> GetByPassport(string passportId);
        
        List<Baggage> GetByStatus(string status);

        Baggage Update(int id, Baggage updatedBaggage);
    }
}