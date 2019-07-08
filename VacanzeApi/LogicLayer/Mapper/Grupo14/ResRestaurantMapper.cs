using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo14;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo14;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo14
{
    public class ResRestaurantMapper : Mapper<ResRestaurantDTO, Restaurant_res>
    {

        public ResRestaurantDTO CreateDTO(Restaurant_res entity)
        {
            Restaurant_res resRest = (Restaurant_res)entity;
            ResRestaurantDTO resRestDTO = DTOFactory.CreateResRestaurantDTO(resRest.rest_id, resRest.cityName, resRest.countryName,
            resRest.restaurantName, resRest.restaurantAddress, resRest.fecha_res, resRest.cant_people);
            return resRestDTO;
        }

        public List<ResRestaurantDTO> CreateDTOList(List<Restaurant_res> entities)
        {
            List<ResRestaurantDTO> dtos = new List<ResRestaurantDTO>();
            foreach (Entity entity in entities)
            {
                Restaurant_res resRest = (Restaurant_res)entity;
                dtos.Add(DTOFactory.CreateResRestaurantDTO(resRest.rest_id, resRest.cityName, resRest.countryName,
            resRest.restaurantName, resRest.restaurantAddress, resRest.fecha_res, resRest.cant_people));
            }
            return dtos;
        }

        public Restaurant_res CreateEntity(ResRestaurantDTO resRestDTO)
        {
            Restaurant_res entity = EntityFactory.CreateResRestaurant(resRestDTO.rest_id, resRestDTO.cityName, resRestDTO.countryName, 
            resRestDTO.restaurantName, resRestDTO.restaurantAddress, resRestDTO.fecha_res, resRestDTO.cant_people);

            return entity;
        }

        public List<Restaurant_res> CreateEntityList(List<ResRestaurantDTO> dtos)
        {
            List<Restaurant_res> entities = new List<Restaurant_res>();
            foreach (ResRestaurantDTO resRestDTO in dtos)
            {
                entities.Add(EntityFactory.CreateResRestaurant(resRestDTO.rest_id, resRestDTO.cityName, resRestDTO.countryName,
            resRestDTO.restaurantName, resRestDTO.restaurantAddress, resRestDTO.fecha_res, resRestDTO.cant_people));
            }
            return entities;
        }
    }
}
