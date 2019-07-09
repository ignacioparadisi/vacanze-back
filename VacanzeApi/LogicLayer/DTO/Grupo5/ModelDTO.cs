

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5{

    public class ModelDTO : DTO{
        
        private int _id;
        public int Id { get { return _id; } set{ _id = value; } }

        private int _brandId;
        public int BrandId { get { return _brandId; } set{ _brandId = value; } }

        private string _modelName;
        public string ModelName { get { return _modelName; } set{ _modelName = value; } }

        private int _capacity;
        public int Capacity { get { return _capacity; } set{ _capacity = value; } }

        private string _picture;
        public string Picture { get { return _picture; } set{ _picture = value; } }

        public ModelDTO(){}

         public ModelDTO(int _brandId, string _modelName, int _capacity){
            this._brandId = _brandId;
            this._modelName = _modelName;
            this._capacity = _capacity;
        }

        public ModelDTO(int _brandId, string _modelName, int _capacity, string _picture){
            this._brandId = _brandId;
            this._modelName = _modelName;
            this._capacity = _capacity;
            this._picture = _picture;
        }

        public ModelDTO(int _id, int _brandId, string _modelName, int _capacity){
            this._id = _id;
            this._brandId = _brandId;
            this._modelName = _modelName;
            this._capacity = _capacity;
        }

         public ModelDTO(int _id, int _brandId, string _modelName, int _capacity, string _picture){
            this._id = _id;
            this._brandId = _brandId;
            this._modelName = _modelName;
            this._capacity = _capacity;
            this._picture = _picture;
        }
    }
}