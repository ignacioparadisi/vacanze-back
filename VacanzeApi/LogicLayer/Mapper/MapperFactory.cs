using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper
{

    public class MapperFactory{

        public static BrandMapper createBrandMapper(){
            return new BrandMapper();
        }

        // +++++++++++++++++
        //     GRUPO 6
        // +++++++++++++++++
        public static HotelMapper createHotelMapper(){
            return new HotelMapper();
        }

    }
}