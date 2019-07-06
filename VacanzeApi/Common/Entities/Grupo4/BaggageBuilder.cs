 
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Common.Entities.Grupo4
{
public class BaggageBuilder{

    private readonly Baggage _baggage;

    private BaggageBuilder(){

        _baggage= new Baggage();

    }
    public static BaggageBuilder Create()
    {
        return new BaggageBuilder();

    }
    public BaggageBuilder IdentifiedBy(int id)
    {
        _baggage._id=id;
        return this;
    }

    public BaggageBuilder WhitNumberOfFlight(int numberOfFlight)
    {
        _baggage._maletaFkVuelo=numberOfFlight;
        return this;
    
    }
    public BaggageBuilder WhitNumberOfCruise(int numberOfCruise)
    {
        _baggage._maletaFkCrucero=numberOfCruise;
        return this;
    
    }
        public BaggageBuilder WhitDescription(string description)
    {
        _baggage._descripcion=description;
        return this;
    
    }
    public Baggage BuildSinValidar()
    {
        return _baggage;
    }
}

}