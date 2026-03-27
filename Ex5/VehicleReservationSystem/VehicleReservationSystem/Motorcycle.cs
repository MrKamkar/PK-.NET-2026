using System;

namespace VehicleReservationSystem
{
    public class Motorcycle : Vehicle
    {
        public int EngineCapacity { get; private set; }

        public Motorcycle(int id, string brand, string model, int year, int engineCapacity) : base(id, brand, model, year)
        {
            EngineCapacity = engineCapacity;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Motocykl: {Brand} {Model} ({Year}), Pojemność: {EngineCapacity}cc, Dostępny: {IsAvailable}");
        }
    }
}
