using System.Collections.Generic;
using NUnit.Framework;
using System.Data;
using System;
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

     /*    [Test]
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
*/
        [Test]
        public void WithoutTravelLocationsException(){
            Assert.Throws<WithoutTravelLocationsException>(
                () => TravelRepository.GetLocationsByTravel(100));
        }

        [Test]
        public void GetLocationsByTravelSuccessfully(){
            List<Location> locations = TravelRepository.GetLocationsByTravel(1);
            Assert.NotZero(locations.Count);
        }

        [Test]
        public void AddTravel_RequiredAttributeException_WithoutName(){
            Travel travel = new Travel(
                "", 
                new DateTime(2019,3,10,0,0,0),
                new DateTime(2019,4,10,0,0,0), 
                "Description",
                5
            );
            Assert.Throws<RequiredAttributeException>(
                () => TravelRepository.AddTravel(travel));
        }

        [Test]
        public void AddTravel_RequiredAttributeException_WithoutDatetime(){
            Travel travel = new Travel(
                "", 
                DateTime.MinValue,
                new DateTime(2019,4,10,0,0,0), 
                "Description",
                5
            );
            Assert.Throws<RequiredAttributeException>(
                () => TravelRepository.AddTravel(travel));
        }

        [Test]
        public void AddTravel_UserNotFoundException(){
            Travel travel = new Travel(
                "Name", 
                new DateTime(2019,3,10,0,0,0),
                new DateTime(2019,4,10,0,0,0), 
                "Description",
                200
            );
            Assert.Throws<UserNotFoundException>(
                () => TravelRepository.AddTravel(travel));
        }

        [Test]
        public void AddTravelSuccessfully(){
            Travel travel = new Travel(
                "Name", 
                new DateTime(2019,3,10,0,0,0),
                new DateTime(2019,4,10,0,0,0), 
                "Description",
                5
            );
            Assert.NotZero(TravelRepository.AddTravel(travel));
        }

    }
}