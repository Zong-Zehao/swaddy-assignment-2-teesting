using System;

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
    }

    // zehao start

    public static Car GetCarById(int carId, List<Car> carList)
    {
        return carList.Find(c => c.CarId == carId);
    }

    public void ScheduleInspection(int carId)
    {
        // Implementation for scheduling a repair
        Console.WriteLine($"\nInspection scheduled for car with ID: {carId}");
    }
    // zehao end

    public override string ToString()
    {
        return $"Car ID: {CarId}, Make: {Make}, Model: {Model}, Year: {Year}, Mileage: {Mileage}, Insurance: {Insurance}, Photos: {Photos}";
    }
}