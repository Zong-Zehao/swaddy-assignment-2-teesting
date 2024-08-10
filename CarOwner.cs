using System;
using System.Collections.Generic;
using SWAD_Assignment_2;

public class CarOwner
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Car> RegisteredCars { get; set; } = new List<Car>();

    public CarOwner(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddNewListing(Car car)
    {
        RegisteredCars.Add(car);
        Console.WriteLine($"Car {car.Make} {car.Model} registered successfully!");
    }

    public void DisplayRegisteredCars()
    {
        Console.WriteLine($"Cars registered by {Name}:");
        foreach (var car in RegisteredCars)
        {
            Console.WriteLine(car);
        }
    }

    public void SetNewRate(Car car, double newRate)
    {
        try
        {
            var rentalRate = new RentalRate(car.CarId, newRate);
            car.SetRentalRate(rentalRate);
            Console.WriteLine($"Updated Rate for {car.Make} {car.Model}: {newRate}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void SetNewSchedule(Car car, DateTime newStartDateTime, DateTime newEndDateTime)
    {
        try
        {
            var schedule = new AvailabilitySchedule(car.CarId);
            schedule.UpdateSchedule(newStartDateTime, newEndDateTime);
            car.SetAvailabilitySchedule(schedule);
            Console.WriteLine($"Updated Schedule for {car.Make} {car.Model} from {newStartDateTime:yyyy-MM-dd} to {newEndDateTime:yyyy-MM-dd}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}


