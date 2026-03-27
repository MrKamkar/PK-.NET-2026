using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleReservationSystem
{
    public static class VehicleExtensions
    {
        public static List<Vehicle> GetAvailableVehicles(this List<Vehicle> vehicles)
        {
            return vehicles.Where(v => v.IsAvailable).ToList();
        }
    }
}
