namespace vacanze_back.VacanzeApi.Common.Entities.Grupo5
{
    public class Auto
    {
        public int _id;
        public string _make;
        public string _model;
        public int _capacity;
        public bool _isActive;
        public string _licence;
        public float _price;
        public int _place;
        
        public Auto (string make,string model,int capacity,bool isactive,string licence,float price,int place)
        {
            this._id= 0;
            this._make =make;
            this._model = model;
            this._capacity= capacity;
            this._isActive=isactive ;
            this._licence = licence;
            this._price=price;
            this._place=place;
        }
        public int getId()
        {
            return _id;
        }
        public void setId(int Id)
        {
            this._id = Id;
        }
        public string getmake(){
            return _make;
        }
        public void setmake(string make){
            this._make=make;
        }
        public string getmodel(){
            return _model;
        }
        public void setmodelo(string model)
        {
            this._model= model;
        }
        public int getcapacity(){
            return _capacity;
        }
        public void setcapPasajero(int capacity){
            this._capacity=capacity;
        }
        public float getprice(){
            return _price;
        }
        public void setprice(float price){
            this._price=price;
        }
        public bool getisActive(){
            return _isActive;
        }
        public void setisactive(bool isactive){
            this._isActive=isactive;
        }
        public string getlicence(){
            return _licence;
        }
        public void setlicence(string licence){
            this._licence=licence;
        }
        public int getplace (){
            return _place;
        }
        public void setplace(int place){
            this._place=place;
        }
    }
}