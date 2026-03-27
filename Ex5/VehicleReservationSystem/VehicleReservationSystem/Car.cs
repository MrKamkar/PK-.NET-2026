using System;

namespace VehicleReservationSystem
{
    public class Car : Vehicle
    {
        public string BodyType { get; private set; }

        public Car(int id, string brand, string model, int year, string bodyType)
            : base(id, brand, model, year)
        {
            BodyType = bodyType;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Samochód: {Brand} {Model} ({Year}), Typ: {BodyType}, Dostępny: {IsAvailable}");
        }
    }
}
