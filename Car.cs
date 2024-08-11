using SWAD_Assignment_2;
using System;
using System.Collections.Generic;

public class Car
{
    public int CarId { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public double Mileage { get; set; }
    public string Insurance { get; set; }
    public string Photos { get; set; }
    public List<DamageReport> DamageReports { get; set; }
    public RentalRate RentalRate { get; set; }
    public AvailabilitySchedule AvailabilitySchedule { get; set; }

    public Car(int carId, string make, string model, int year, double mileage, string insurance, string photos)
    {
        CarId = carId;
        Make = make;
        Model = model;
        Year = year;
        Mileage = mileage;
        Insurance = insurance;
        Photos = photos;
        DamageReports = new List<DamageReport>();
        RentalRate = new RentalRate(carId, 50);
        AvailabilitySchedule = new AvailabilitySchedule(carId); // Initialize AvailabilitySchedule
    }

    // Method to retrieve a car by its ID from a list 
    public static Car GetCarById(int carId, List<Car> carList)
    {
        return carList.Find(c => c.CarId == carId);
    }

    // Method to schedule an inspection for the car 
    public void ScheduleInspection()
    {
        // Implementation for scheduling a repair 
        Console.WriteLine($"\nInspection scheduled for car with ID: {CarId}");
    }

    // Method to set the rental rate for the car 
    public void SetRentalRate(RentalRate rentalRate)
    {
        if (rentalRate.CarId == this.CarId)
        {
            RentalRate = rentalRate;
            Console.WriteLine("Rental rate set successfully.");
        }
        else
        {
            Console.WriteLine("Error: Invalid");
        }
    }

    // Method to set the availability schedule for the car 
    public void SetAvailabilitySchedule(AvailabilitySchedule schedule)
    {
        if (schedule.CarId == this.CarId)
        {
            AvailabilitySchedule = schedule;
            Console.WriteLine("Availability schedule set successfully.");
        }
        else
        {
            Console.WriteLine("Error: Schedule's CarId does not match this car.");
        }
    }

    // New method to add a time period to the availability schedule
    public void SetNewSchedule(DateTime startDate, DateTime endDate)
    {
        AvailabilitySchedule.AddTimePeriod(startDate.Date, endDate.Date);
    }

    // Method to get the rental rate for the car 
    public RentalRate GetRentalRate()
    {
        return RentalRate;
    }

    // Method to get the availability schedule for the car 
    public AvailabilitySchedule GetAvailabilitySchedule()
    {
        return AvailabilitySchedule;
    }

    public override string ToString()
    {
        string rentalRateDisplay = RentalRate != null ? $"Rental Rate: ${RentalRate.Rate}" : "Rental Rate: Not Set";
        return $"Car ID: {CarId}, Make: {Make}, Model: {Model}, Year: {Year}, Mileage: {Mileage}, Insurance: {Insurance}, Photos: {Photos}, {rentalRateDisplay}";
    }
}

