using NUnit.Framework;
using System.Collections.Generic;
using VehicleReservationSystem;

namespace VehicleReservationSystemTests
{
    public class ReservationSystemTests
    {
        [Test]
        public void Vehicle_Creation_AttributesAreCorrectlySet()
        {
            var car = new Car(1, "Toyota", "Corolla", 2020, "Sedan");
            Assert.That(car.Id, Is.EqualTo(1));
            Assert.That(car.Brand, Is.EqualTo("Toyota"));
            Assert.That(car.Model, Is.EqualTo("Corolla"));
            Assert.That(car.Year, Is.EqualTo(2020));
            Assert.That(car.BodyType, Is.EqualTo("Sedan"));
            Assert.That(car.IsAvailable, Is.True);

            var moto = new Motorcycle(2, "Yamaha", "MT-07", 2021, 689);
            Assert.That(moto.Id, Is.EqualTo(2));
            Assert.That(moto.EngineCapacity, Is.EqualTo(689));
        }

        [Test]
        public void Reservation_VehicleReserved_IsAvailableBecomesFalse()
        {
            var car = new Car(1, "Toyota", "Corolla", 2020, "Sedan");
            car.Reserve("John Doe");

            Assert.That(car.IsAvailable, Is.False);
        }

        [Test]
        public void Reservation_CancelReservation_IsAvailableBecomesTrue()
        {
            var car = new Car(1, "Toyota", "Corolla", 2020, "Sedan");
            car.Reserve("John Doe");
            car.CancelReservation();

            Assert.That(car.IsAvailable, Is.True);
        }

        [Test]
        public void Extensions_GetAvailableVehicles_ReturnsOnlyAvailableVehicles()
        {
            var list = new List<Vehicle>
            {
                new Car(1, "Toyota", "Corolla", 2020, "Sedan"),
                new Car(2, "Honda", "Civic", 2018, "Hatchback"),
                new Motorcycle(3, "Yamaha", "MT-07", 2021, 689)
            };

            list[0].Reserve("John");
            
            var available = list.GetAvailableVehicles();

            Assert.That(available.Count, Is.EqualTo(2));
            Assert.That(available[0].Id, Is.EqualTo(2));
            Assert.That(available[1].Id, Is.EqualTo(3));
        }

        [Test]
        public void RentalCompany_OnNewReservation_EventIsFired()
        {
            var company = new RentalCompany();
            company.AddVehicle(new Car(1, "Toyota", "Corolla", 2020, "Sedan"));
            
            bool eventFired = false;
            string eventMessage = "";
            company.OnNewReservation += (msg) => {
                eventFired = true;
                eventMessage = msg;
            };

            company.ReserveVehicle(1, "Alice");

            Assert.That(eventFired, Is.True);
            Assert.That(eventMessage.Contains("Alice"), Is.True);
            Assert.That(eventMessage.Contains("1"), Is.True);
        }
    }
}
