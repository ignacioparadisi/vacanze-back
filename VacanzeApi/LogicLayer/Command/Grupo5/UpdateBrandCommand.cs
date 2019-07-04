using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 {

    public class UpdateBrandCommand : Command, CommandResult<bool> {
        private bool _updated;
        public bool Updated { get{ return _updated; } set{ _updated = value; } }

        private Brand _brand;
        public Brand Brand { get{ return _brand; } set{ _brand = value; } }

        public UpdateBrandCommand(Brand brand){
            this._brand = brand;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IBrandDAO brandDAO = daoFactory.GetBrandDAO();
            _updated = brandDAO.UpdateBrand(_brand);
        }

        public bool GetResult(){
            return _updated;
        }

    }

}