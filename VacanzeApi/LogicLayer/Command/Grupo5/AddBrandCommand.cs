using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 {

    public class AddBrandCommand : Command, CommandResult<int> {
        private int _id;
        private Brand _brand;

        public AddBrandCommand (Brand _brand) {
            this._brand = _brand;
        }

        public void Execute () {
            DAOBrand daoBrand = new DAOBrand (); //Without Factory
            _id = daoBrand.AddBrand (_brand);
        }

        public int GetResult () {
            return _id;
        }

    }

}