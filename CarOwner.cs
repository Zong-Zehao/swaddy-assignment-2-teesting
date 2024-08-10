public class CarOwner
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Car> RegisteredCars { get; set; } = new List<Car>();

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
}

