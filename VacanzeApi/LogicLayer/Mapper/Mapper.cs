using vacanze_back.VacanzeApi.Common.Entities;
using System.Collections.Generic;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper{

    public interface Mapper <T,E> {

        T CreateDTO(E entity);

        E CreateEntity(T dto);

        List<T> CreateDTOList(List<E> entities);

        List<E> CreateEntityList(List<T> dtos);
    }
}