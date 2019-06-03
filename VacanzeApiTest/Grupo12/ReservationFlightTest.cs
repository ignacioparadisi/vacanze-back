using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo12;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo12;

namespace vacanze_back.VacanzeApiTest.Grupo12
{   
    [TestFixture]
    
    public class ReservationFlightTest{
       
		FlightResConnection f= new FlightResConnection();

        [Test,  Order(1)]
		public void AddReservationFlightTest() {
            FlightRes user=new FlightRes("A-1","2019-01-01",1,1,1);
            int id_user=f.AddReservationFlight(user);
			Assert.NotNull(id_user);
		}

        [Test,  Order(2)]
		public void GetReservationFlightTest() {
            List<FlightRes> getreservation=f.GetReservationFlight(1);
			Assert.True(getreservation.Count>0);
		}

        [Test,  Order(3)]
		public void GetIDLocationTest() {
            string namecity="Valera";
            int id=f.GetIDLocation(namecity);
			Assert.True(id>0);
		}

        
        [Test,  Order(4)]
		public void GetFlightValidateITest() {
            List<ListRes> id=f. GetFlightValidateI(5,6,"2019-03-01",1);
			Assert.True(id.Count>0);
		}

        [Test,  Order(5)]
		public void conSeatNumTest() {
            string seat=f.conSeatNum(2,1);
			Assert.IsNotNull(seat);
		}

        [Test,  Order(6)]
		public void GetReservationFlightIVTest() {
           List<ListRes> seat=f.GetReservationFlightIV(5,6,"2019-03-01","2040-03-01",1);
		   Assert.IsFalse(seat.Count>0);
		}

         [Test,  Order(7)]
		public void GetFlightValidateIExceptionTest() {

            Assert.Throws<EmptyListReservation>(
                () => f.GetReservationFlight(9)); 
            }

    }
}
