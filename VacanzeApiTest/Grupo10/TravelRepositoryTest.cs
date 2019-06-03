using System.Collections.Generic;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Persistence.Repository;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo10;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo10;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo10;

namespace vacanze_back.VacanzeApiTest.Grupo10
{   
    [TestFixture]
    public class TravelRepositoryTest{

        [Test]
        public void UserNotFoundException(){
            Assert.Throws<UserNotFoundException>(
                () => TravelRepository.GetTravels(1000));
        }

        [Test]
        public void WithoutExistenceOfTravelsException(){
            Assert.Throws<WithoutExistenceOfTravelsException>(
                () => TravelRepository.GetTravels(1)); //Usuario administrador
        }

        [Test]
        public void GetTravelSuccessfully(){
            List<Travel> listOfTravels = TravelRepository.GetTravels(5); //Usuario cliente
            Assert.NotZero(listOfTravels.Count);
        }

        [Test]
        public void InvalidReservationTypeException(){
            Assert.Throws<InvalidReservationTypeException>(
                () => TravelRepository.GetReservationsByTravelAndLocation<object>(1, 37, "Samuel"));
        }

        [Test]
        public void GetReservationsByTravelAndLocation_HotelSuccessfully(){
            List<object> reservations = 
                TravelRepository.GetReservationsByTravelAndLocation<object>(1,37,"HOTEL");
            Assert.NotZero(reservations.Count);
        }

        [Test]
        public void WithoutTravelReservationsException_Hotel(){
            Assert.Throws<WithoutTravelReservationsException>(
                () => TravelRepository.GetReservationsByTravelAndLocation<object>(2, 37, "HOTEL"));
        }
    }
}