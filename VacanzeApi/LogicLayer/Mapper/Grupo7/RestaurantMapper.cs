using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7
{
    public class RestaurantMapper : Mapper<RestaurantDTO>
    {
        public RestaurantDTO CreateDTO(Entity entity)
        {
            var restaurant = (Restaurant) entity;
            return new RestaurantDTO
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Capacity = restaurant.Capacity,
                IsActive = restaurant.IsActive,
                Qualify = restaurant.Qualify,
                Specialty = restaurant.Specialty,
                Price = restaurant.Price,
                BusinessName = restaurant.BusinessName,
                Picture = restaurant.Picture,
                Description = restaurant.Description,
                Phone = restaurant.Phone,
                Location = restaurant.Location,
                Address = restaurant.Address
            };
        }
        
        public List<RestaurantDTO> CreateDTOList(List<Entity> entities)
        {
            List<RestaurantDTO> dtoList= new List<RestaurantDTO>();
            foreach (Entity entity in entities)
            {
                var restaurant = (Restaurant) entity;
                dtoList.Add(new RestaurantDTO
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Capacity = restaurant.Capacity,
                    IsActive = restaurant.IsActive,
                    Qualify = restaurant.Qualify,
                    Specialty = restaurant.Specialty,
                    Price = restaurant.Price,
                    BusinessName = restaurant.BusinessName,
                    Picture = restaurant.Picture,
                    Description = restaurant.Description,
                    Phone = restaurant.Phone,
                    Location = restaurant.Location,
                    Address = restaurant.Address
                });
            }
            return dtoList;
        }

        public Entity CreateEntity(RestaurantDTO dto)
        {
            return EntityFactory.CreateRestaurant(dto.Id, dto.Name, dto.Capacity, dto.IsActive, dto.Qualify,
                dto.Specialty, dto.Price, dto.BusinessName, dto.Picture, dto.Description, dto.Phone, dto.Location
                ,dto.Address);
        }

        public List<Entity> CreateEntityList(List<RestaurantDTO> dtoList)
        {
            List<Entity> restaurants = new List<Entity>();
            foreach (RestaurantDTO dto in dtoList)
            {
                restaurants.Add(
                    (Restaurant) EntityFactory.CreateRestaurant(dto.Id, dto.Name, dto.Capacity, dto.IsActive, dto.Qualify,
                    dto.Specialty, dto.Price, dto.BusinessName, dto.Picture, dto.Description, dto.Phone, dto.Location
                    ,dto.Address));
            }
            return restaurants;
        }
    }
}