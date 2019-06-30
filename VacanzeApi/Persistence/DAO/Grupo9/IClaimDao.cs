using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo9
{
    public interface IClaimDao
    {
        Claim GetById(int id);
        List<Claim> GetByStatus(string status);
        List<Claim> GetByDocument(string document);
        void Add(Claim claim);
        void Delete(int id);
        void Update(int id, Claim updatedFields);
    }
}