using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;

namespace DefaultNamespace
{
    public interface ICruiserDAO
    {
        List<Cruiser> GetCruisers();
        Cruiser GetCruiser(int shipId);
        int AddCruiser(Cruiser cruiser);
        Cruiser UpdateCruiser(Cruiser cruiser);
        int DeleteCruiser(int id);
        List<Layover> GetLayovers(int cruiserId);
        Layover AddLayover(Layover layover);
        int DeleteLayover(int id);
        List<Layover> GetLayoversForRes(int departure, int arrival);

    }
}