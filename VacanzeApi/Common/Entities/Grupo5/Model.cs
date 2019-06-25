namespace vacanze_back.VacanzeApi.Common.Entities.Grupo5
{
    public class Model : Entity{

        private Brand _modelBrand;
        public Brand ModelBrand { get{ return _modelBrand; } set{ _modelBrand = value; } }
        
        private int _modelBrandId;
        public int ModelBrandId { get{ return _modelBrandId;} set{ _modelBrandId = value; } }

        private string _modelName;
        public string ModelName { get{ return _modelName;} set{ _modelName = value; } }

        private int _capacity;
        public int Capacity { get { return _capacity;} set{ _capacity = value; } }

        private string _picture;
        public string Picture { get { return _picture; } set{ _picture = value; } }

        public Model(int _modelBrandId, string _modelName, int _capacity) :base(0){
            ModelBrandId = _modelBrandId;
            ModelName = _modelName;
            Capacity = _capacity;
        }

        public Model(int _modelBrandId, string _modelName, int _capacity, string _picture) :base(0){
            ModelBrandId = _modelBrandId;
            ModelName = _modelName;
            Capacity = _capacity;
            Picture = _picture;
        }

        public Model(Brand _modelBrand, string _modelName, int _capacity) :base(0){
            ModelBrand = _modelBrand;
            ModelName = _modelName;
            Capacity = _capacity;
        }

        public Model(Brand _modelBrand, string _modelName, int _capacity, string _picture) :base(0){
            ModelBrand = _modelBrand;
            ModelName = _modelName;
            Capacity = _capacity;
            Picture = _picture;
        }

        public Model(int Id, int _modelBrandId, string _modelName, int _capacity) :base(Id){
            this.Id = Id;
            ModelBrandId = _modelBrandId;
            ModelName = _modelName;
            Capacity = _capacity;
        }

        public Model(int Id, int _modelBrandId, string _modelName, int _capacity, string _picture) :base(Id){
            this.Id = Id;
            ModelBrandId = _modelBrandId;
            ModelName = _modelName;
            Capacity = _capacity;
            Picture = _picture;
        }

        public Model(int Id, Brand _modelBrand, string _modelName, int _capacity) :base(Id){
            this.Id = Id;
            ModelBrand = _modelBrand;
            ModelName = _modelName;
            Capacity = _capacity;
        }

        public Model(int Id, Brand _modelBrand, string _modelName, int _capacity, string _picture) :base(Id){
            this.Id = Id;
            ModelBrand = _modelBrand;
            ModelName = _modelName;
            Capacity = _capacity;
            Picture = _picture;
        }
    }
}