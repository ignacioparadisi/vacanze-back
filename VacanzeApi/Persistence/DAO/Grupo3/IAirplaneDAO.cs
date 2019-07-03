using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo3
{
    public interface IAirplaneDAO
    {
       List<Entity> Get();
       Entity Find(long id);
    }
}