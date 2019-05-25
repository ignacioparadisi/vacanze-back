namespace vacanze_back.Entities.Grupo5
{
    public class Auto
    {
        private int _id;
        private string _make;
        private string _model;
        private int _capacity;
        private bool _isActive;
        private string _licence;
        private int _price;
        
        public Auto (string make , string model , int capacity, bool isactive ,string licence , int price )
        {
            this._id= 0;
            this._make =make;
            this._model = model;
            this._capacity= capacity;
            this._isActive=isactive ;
            this._licence = licence;
            this._price=price;
        }
        public int getId()
        {
            return _id;
        }
        public void seId(int Id)
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
        public int getprice(){
            return _price;
        }
        public void setprice(int price){
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

    }



}