using System;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo5 {
    public class Brand : Entity {

        private string _brandName;
        public string BrandName 
        { 
            get { return _brandName; } 
            set 
            { 
                if(value == null || value.Equals(""))
                    throw new RequiredAttributeException("Debe indicar el nombre de la marca");
                else
                    _brandName = value; 
            } 
        }

        public Brand (string brandName) : base (0) {
            this.BrandName = brandName;
        }

        public Brand (int Id, string _brandName) : base (Id) {
            this.Id = Id;
            BrandName = _brandName;
        }

        public Brand () : base (0) { }
    }
}