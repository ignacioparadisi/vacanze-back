using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace DefaultNamespace
{
    public class PostgresRestaurantDAO: IRestaurantDAO
    {
        public int DeleteRestaurant(int id)
        {
            try
            {
                var table = PgConnection.Instance.ExecuteFunction("DeleteRestaurant(@id)",id);
                var deletedid = Convert.ToInt32(table.Rows[0][0]);
                return deletedid;
            }
            catch (InvalidCastException)
            {
                throw new DeleteRestaurantException("El restaurant no existe");
            }
            catch (NpgsqlException e)
            {
                e.ToString();
                throw;
            }
        }
    }
}