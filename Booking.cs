using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAD_Assignment_2;

    public class Booking
    {
        private int bookingId;
        private DateTime startDateTime;
        private DateTime endDateTime;
        private string pickupOption;
        private int totalCost;

        private Car car;
        private Renter renter;

        public Booking(int bookingId, DateTime startDateTime, DateTime endDateTime, string pickupOption, int totalCost, Car car, Renter renter)
        {
            this.bookingId = bookingId;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
            this.pickupOption = pickupOption;
            this.totalCost = totalCost;
            this.car = car;
            this.renter = renter;
        }

        public int BookingId
        {
            get { return bookingId; }
            set { bookingId = value; }
        }

        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set { startDateTime = value; }
        }

        public DateTime EndDateTime
        {
            get { return endDateTime; }
            set { endDateTime = value; }
        }

        public string PickupOption
        {
            get { return pickupOption; }
            set { pickupOption = value; }
        }

        public int TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        public Car Car
        {
            get { return car; }
            set { car = value; }
        }

        public Renter Renter
        {
            get { return renter; }
            set { renter = value; }
        }

        public void displayBookingDetails()
        {
            Console.WriteLine($"Renter: {Renter?.Name}");
            Console.WriteLine($"Car ID: {Car?.CarId}");
            Console.WriteLine($"Booking Period: {StartDateTime:yyyy-MM-dd} - {EndDateTime:yyyy-MM-dd}");
            Console.WriteLine($"Pickup Option: {PickupOption}");
            Console.WriteLine($"Total Cost: ${TotalCost}");
        }

        public void displayPickup()
        {
            if (PickupOption.ToLower() == "delivery")
            {
                Console.WriteLine($"Delivery Address: {Renter.Address}");
            }
            else
            {
                Console.WriteLine($"Pickup Location: {Car?.iCarStation.name}");
            }
        }

        public override string ToString()
        {
            return $"Booking ID: {bookingId}, Start: {startDateTime}, End: {endDateTime}, " +
                   $"Pickup Option: {pickupOption}, Total Cost: {totalCost}, Car ID: {car?.CarId}, Renter: {renter?.Name}";
        }
    }


