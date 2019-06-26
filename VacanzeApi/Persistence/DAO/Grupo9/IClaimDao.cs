using vacanze_back.VacanzeApi.Common.Entities.Grupo9;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo9
{
    public interface IClaimDao
    {
        Claim GetById(int id);
        void Add(Claim claim);
        void Delete(int id);
        void Update(int id, Claim updatedFields);
    }
}