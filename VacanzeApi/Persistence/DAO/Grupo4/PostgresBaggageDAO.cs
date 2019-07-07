using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
 
namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo4
{
    public interface BaggageDAO
    {
        int AddBaggage(CheckinBaggage baggage);
        List<CheckinBaggage> GetBaggage();
        CheckinBaggage GetBaggageById(int id);
        List<CheckinBaggage> GetHotelsByflight(int Flight);
        void DeleteBaggage(int id);
        CheckinBaggage UpdateBaggage(int id, CheckinBaggage newData);
        
    }
}