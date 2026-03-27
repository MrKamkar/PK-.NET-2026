using System;

namespace VehicleReservationSystem
{
    public class Reservation
    {
        public int VehicleId { get; private set; }
        public string CustomerName { get; private set; }
        public DateTime ReservationDate { get; private set; }

        public Reservation(int vehicleId, string customerName)
        {
            VehicleId = vehicleId;
            CustomerName = customerName;
            ReservationDate = DateTime.Now;
        }
    }
}
