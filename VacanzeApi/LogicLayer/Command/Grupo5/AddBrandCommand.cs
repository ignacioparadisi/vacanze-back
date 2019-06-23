using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 {

    public class AddBrandCommand : Command, CommandResult<int> {
        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private Brand _brand;
        public Brand Brand { get{ return _brand; } set{ _brand = value; } }

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