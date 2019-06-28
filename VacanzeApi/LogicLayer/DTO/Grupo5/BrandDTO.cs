using Newtonsoft.Json;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5{

    public class BrandDTO : DTO {

        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private string _brandName;
        public string BrandName { get { return _brandName; } set{ _brandName = value; } }

        [JsonConstructor]
        public BrandDTO(string _brandName){
            this.BrandName = _brandName;
        }

        public BrandDTO(int _id, string _brandName){
            this.Id = _id;
            this._brandName = _brandName;
        }
    }
}