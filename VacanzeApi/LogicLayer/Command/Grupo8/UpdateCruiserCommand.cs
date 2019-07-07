using System.Collections.Generic;
using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;
using vacanze_back.VacanzeApi.Persistence.DAO;


namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8
{
    /// <summary>  
    ///  Comando para actualizar un Crucero
    /// </summary> 
    public class UpdateCruiserCommand : CommandResult<CruiserDto>
    {
        private CruiserDto _cruiserDto;
        public UpdateCruiserCommand(CruiserDto cruiserDto)
        {
            _cruiserDto = cruiserDto;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            CruiserMapper cruiserMapper = MapperFactory.CreateCruiserMapper();
            Cruiser cruiser = cruiserMapper.CreateEntity(_cruiserDto);
            CommandFactory.CreateGetCruiserValidatorCommand(cruiser).Execute();
            _cruiserDto = cruiserMapper.CreateDTO(daoFactory.GetCruiserDAO().UpdateCruiser(cruiser));
        }
        public CruiserDto GetResult()
        {
            return _cruiserDto;
        }
    }
}