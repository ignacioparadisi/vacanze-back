using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo3
{
    public interface IAirplaneDAO
    {
        List<Airplane> Get();
        Airplane Find(long id);
    }
}