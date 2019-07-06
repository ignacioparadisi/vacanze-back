using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
 
namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo4
{
    public interface BaggageDAO
    {
        int AddBaggage(Baggage baggage);
        List<Baggage> GetBaggage();
        Baggage GetBaggageById(int id);
        List<Baggage> GetHotelsByflight(int Flight);
        void DeleteBaggage(int id);
        Baggage UpdateBaggage(int id, Baggage newData);
        
    }
}