using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo3
{
    public interface IFlightDAO
    {
       List<Entity> Get();
       int Add(Entity entity);
       void Update(Entity entity);
       void Delete(Entity entity);
       Entity Find(int id);
       List<Entity> GetByDate(string begin, string end);
       List<Entity> GetByLocation(int departure, int arrival);
       List<Entity> GetOutboundFlights(int departure, int arrival, string departuredate);
       List<Entity> GetRoundTripFlights(int departure, int arrival, string departuredate, string arrivaldate);

       Flight GetRow(DataTable table, int i);
    }
}