namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3
{
    public class AirplaneDTO: DTO {

        
        public string model {get; set;}
        public double loadCapacity {get; set;}
        public int seats {get; set;}
        public double autonomy {get; set;}
        public bool isActive { get; set; }

        public AirplaneDTO(){
            
        }

        public AirplaneDTO(int id){
            
        }
        
        public AirplaneDTO(int id, string model, double load_capacity, int seats, double autonomy, bool isActive){
            this.model = model;
            this.loadCapacity = load_capacity;
            this.autonomy = autonomy;
            this.seats = seats;
            this.isActive = isActive;
        }

        public void setModel(string model){
            this.model = model;
        }

        public void setLoadCapacity(double loadCapacity){
            this.loadCapacity = loadCapacity;
        }

        public void setAutonomy(double autonomy){
            this.autonomy = autonomy;
        }

        public void setSeats(int seats){
            this.seats = seats;
        }

    }
}