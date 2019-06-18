namespace vacanze_back.VacanzeApi.Common.Entities.Grupo5
{
    public class Brand : Entity{

        private string _brandName;
        public string BrandName{ get { return _brandName; } set { _brandName = value; } }

        public Brand(string _brandName) :base(0){
            this.BrandName = _brandName;
        }

        public Brand(int Id, string _brandName) :base(Id){
            this.Id = Id;
            BrandName = _brandName;
        }
    }
}