using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleReservationSystem
{
    public class RentalCompany
    {
        private List<Vehicle> _vehicles = new List<Vehicle>();
        private List<Reservation> _reservations = new List<Reservation>();

        public event Action<string>? OnNewReservation;

        public void AddVehicle(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
        }

        public void ReserveVehicle(int vehicleId, string customer)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.Id == vehicleId);
            if (vehicle != null)
            {
                if (vehicle.IsAvailable)
                {
                    vehicle.Reserve(customer);
                    _reservations.Add(new Reservation(vehicleId, customer));
                    OnNewReservation?.Invoke($"Nowa rezerwacja: {customer} zarezerwował pojazd o ID {vehicleId}.");
                }
                else
                {
                    Console.WriteLine("Pojazd jest niedostępny.");
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono pojazdu.");
            }
        }

        public void CancelReservation(int vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.Id == vehicleId);
            if (vehicle != null && !vehicle.IsAvailable)
            {
                vehicle.CancelReservation();
                var reservation = _reservations.FirstOrDefault(r => r.VehicleId == vehicleId);
                if (reservation != null)
                {
                    _reservations.Remove(reservation);
                }
                Console.WriteLine($"Anulowano rezerwację pojazdu o ID {vehicleId}.");
            }
            else
            {
                Console.WriteLine("Nie można anulować rezerwacji. Pojazd jest dostępny lub nie istnieje.");
            }
        }

        public void ListAvailableVehicles()
        {
            var availableVehicles = _vehicles.GetAvailableVehicles();
            Console.WriteLine("Dostępne pojazdy:");
            foreach (var vehicle in availableVehicles)
            {
                vehicle.DisplayInfo();
            }
        }
    }
}
