using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class GetModelsByBrandCommand : CommandResult<List<Model>> 
    {   
        private int _brandId;
        public int BrandId { get{ return _brandId; } set{ _brandId = value; } }

        private List<Model> models;

        public GetModelsByBrandCommand(int _brandId){
            this.BrandId = _brandId;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IModelDAO modelDAO = daoFactory.GetModelDAO();
            models = modelDAO.GetModelsByBrand(_brandId);
        }

        public List<Model> GetResult(){
            return models;
        }
    }
}