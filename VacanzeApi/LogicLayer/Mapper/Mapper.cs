using vacanze_back.VacanzeApi.Common.Entities;
using System.Collections.Generic;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper{

    public interface Mapper <T> {

        T CreateDTO(Entity entity);

        Entity CreateEntity(T dto);

        List<T> CreateDTOList(List<Entity> entities);

        List<Entity> CreateEntityList(List<T> dtos);

    }
}