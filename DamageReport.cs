public class DamageReport
{
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public Car Car { get; set; }
    public string ThirdPartyDamage { get; set; }
    public string ThirdPartyInjury { get; set; }
    public string ScratchOnCar { get; set; }
    public string DentOnCar { get; set; }
    public int NumberOfScratches { get; set; } // Added property
    public string ScratchLocation { get; set; } // Added property
    public string DentLocation { get; set; }
    public string BrokenWindows { get; set; }
    public string BrokenWindowLocation { get; set; }
    public string CarSeverelyDestroyed { get; set; }
    public string DestroyedPart { get; set; }
    public string CarBrokeDown { get; set; }
    public string FlatTire { get; set; }
    public string NeedTowing { get; set; }

    public void DisplayReport()
    {
        Console.WriteLine($"User ID: {UserId}");
        Console.WriteLine($"Car ID: {CarId}");
        Console.WriteLine($"Car Make: {Car?.Make}");
        Console.WriteLine($"Car Model: {Car?.Model}");
        Console.WriteLine($"Car Year: {Car?.Year}");
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
        Console.WriteLine($"Time: {Time}");
        Console.WriteLine($"Location: {Location}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Third Party Damage: {ThirdPartyDamage}");
        Console.WriteLine($"Third Party Injury: {ThirdPartyInjury}");
        Console.WriteLine($"Scratch on Car: {ScratchOnCar}");
        Console.WriteLine($"Number of Scratches: {NumberOfScratches}");
        Console.WriteLine($"Dent on Car: {DentOnCar}");
        Console.WriteLine($"Dent Location: {DentLocation}");
        Console.WriteLine($"Broken Windows: {BrokenWindows}");
        Console.WriteLine($"Broken Window Location: {BrokenWindowLocation}");
        Console.WriteLine($"Car Severely Destroyed: {CarSeverelyDestroyed}");
        Console.WriteLine($"Destroyed Part: {DestroyedPart}");
        Console.WriteLine($"Car Broke Down: {CarBrokeDown}");
        Console.WriteLine($"Flat Tire: {FlatTire}");
        Console.WriteLine($"Need Towing / road-side assistance: {NeedTowing}");
    }
}