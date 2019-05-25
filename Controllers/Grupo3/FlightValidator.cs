using vacanze_back.Entities.Grupo3;
using vacanze_back.Entities;

namespace vacanze_back.Controllers.Grupo3
{
    public class FlightValidator
    {
        public Flight flight { get; set; }

        public FlightValidator(Flight flight){
            this.flight = flight;
        }

        public void Validate(){

        }

    }
}