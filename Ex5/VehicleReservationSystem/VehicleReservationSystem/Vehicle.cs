using System;

namespace VehicleReservationSystem
{
    public abstract class Vehicle : IReservable
    {
        public int Id { get; protected set; }
        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public int Year { get; protected set; }
        public bool IsAvailable { get; protected set; }

        public Vehicle(int id, string brand, string model, int year)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            IsAvailable = true; // Pojazd domyślnie dostępny
        }

        public abstract void DisplayInfo();

        public virtual void Reserve(string customer)
        {
            if (!IsAvailable)
                throw new InvalidOperationException("Pojazd jest już zarezerwowany.");
            
            IsAvailable = false;
        }

        public virtual void CancelReservation()
        {
            IsAvailable = true;
        }

        bool IReservable.IsAvailable()
        {
            return IsAvailable;
        }
    }
}
