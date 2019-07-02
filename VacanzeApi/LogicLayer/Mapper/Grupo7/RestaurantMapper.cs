using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7
{
    public class RestaurantMapper : Mapper<RestaurantDTO,Restaurant>
    {
        public RestaurantDTO CreateDTO(Restaurant restaurant)
        {
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
        
        public List<RestaurantDTO> CreateDTOList(List<Restaurant> restaurants)
        {
            List<RestaurantDTO> dtoList= new List<RestaurantDTO>();
            foreach (Restaurant restaurant in restaurants)
            {
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

        public Restaurant CreateEntity(RestaurantDTO dto)
        {
            return EntityFactory.CreateRestaurant(dto.Id, dto.Name, dto.Capacity, dto.IsActive, dto.Qualify,
                dto.Specialty, dto.Price, dto.BusinessName, dto.Picture, dto.Description, dto.Phone, dto.Location
                ,dto.Address);
        }

        public List<Restaurant> CreateEntityList(List<RestaurantDTO> dtoList)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            foreach (RestaurantDTO dto in dtoList)
            {
                restaurants.Add(
                     EntityFactory.CreateRestaurant(dto.Id, dto.Name, dto.Capacity, dto.IsActive, dto.Qualify,
                    dto.Specialty, dto.Price, dto.BusinessName, dto.Picture, dto.Description, dto.Phone, dto.Location
                    ,dto.Address));
            }
            return restaurants;
        }
    }
}