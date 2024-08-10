using System.Diagnostics.Metrics;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the iCar System");
        Renter renter = new Renter("DL123", Renter.RenterType.Prime, 1, "John Doe", 1234567890, new DateTime(1980, 1, 1), "123 Main St");
        CarOwner carOwner = new CarOwner { Id = 1, Name = "John Doe" };

        string currentUserType = "Car Owner"; // Default to Car Owner view

        while (true)
        {
            Console.WriteLine($"\nCurrent View: {currentUserType}");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Switch to Car Owner View");
            Console.WriteLine("2. Switch to Renter View");

            if (currentUserType == "Car Owner")
            {
                Console.WriteLine("3. Register a Vehicle");
                Console.WriteLine("4. Display Registered Vehicles");
            }
            else if (currentUserType == "Renter")
            {
                Console.WriteLine("3. Damage Report");
            }

            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            int option;
            while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 5 ||
                   (currentUserType == "Renter" && option == 4) ||
                   (currentUserType == "Car Owner" && option == 3 && currentUserType != "Car Owner"))
            {
                Console.WriteLine("Invalid option. Please enter a valid number.");
                Console.Write("Choose an option: ");
            }

            switch (option)
            {
                case 1:
                    currentUserType = "Car Owner";
                    break;
                case 2:
                    currentUserType = "Renter";
                    break;
                case 3:
                    if (currentUserType == "Car Owner")
                    {
                        RegisterVehicle(carOwner);
                    }
                    else if (currentUserType == "Renter")
                    {
                        SubmitDamageReport(renter, carOwner);
                    }
                    break;
                case 4:
                    if (currentUserType == "Car Owner")
                    {
                        carOwner.DisplayRegisteredCars();
                    }
                    break;
                case 5:
                    Console.WriteLine("Exiting the system. Goodbye!");
                    return;
            }
        }
    }

    // zehao start
    static void SubmitDamageReport(Renter renter, CarOwner carOwner)
    {
        int carId;
        DateTime date;
        TimeSpan time;
        string location;
        string description;
        Car car;

        while (true)
        {
            // Prompt for all details including car ID
            carId = PromptForInt("Enter the car ID: ");
            date = PromptForDate("Enter the date of the damage (yyyy-mm-dd): ");
            time = PromptForTime("Enter the time of the damage (hh:mm): ");
            location = PromptForString("Enter the location of the damage: ");
            description = PromptForString("Enter the description of the accident: ");

            // Validate car ID with the car owner's registered car list
            car = Car.GetCarById(carId, carOwner.RegisteredCars);

            if (car != null)
            {
                break; // Exit the loop when a valid car ID is found
            }
            else
            {
                Console.WriteLine("Invalid Car ID. Please enter a valid Car ID.");
            }
        }

        // Collect additional damage details
        string thirdPartyDamage = PromptForString("Damage to third party property (specify what property the car damaged, if any): ");
        string thirdPartyInjury = PromptForString("Death/Injury to third party (specify if any): ");
        string scratchOnCar = PromptForYesNo("Are there any scratches on the car? (yes/no): ");
        int numberOfScratches = 0;
        if (scratchOnCar.ToLower() == "yes")
        {
            numberOfScratches = PromptForInt("How many scratches are there? ");
        }
        string dentOnCar = PromptForYesNo("Is there a dent on the car? (yes/no): ");
        string dentLocation = string.Empty;
        if (dentOnCar.ToLower() == "yes")
        {
            dentLocation = PromptForString("Specify the area of the dent (Hood area, left side, right side, trunk area, top of car): ");
        }
        string brokenWindows = PromptForYesNo("Are any windows broken? (yes/no): ");
        string brokenWindowLocation = string.Empty;
        if (brokenWindows.ToLower() == "yes")
        {
            brokenWindowLocation = PromptForString("Specify which window is broken (windshield, front passenger, driver, back passenger, rear window): ");
        }
        string carSeverelyDestroyed = PromptForYesNo("Is the car severely destroyed? (yes/no): ");
        string destroyedPart = string.Empty;
        if (carSeverelyDestroyed.ToLower() == "yes")
        {
            destroyedPart = PromptForString("Specify which part is destroyed (front, back, left, right): ");
        }
        string carBrokeDown = PromptForYesNo("Did the car break down? (yes/no): ");
        string flatTire = PromptForYesNo("Is there a flat tire? (yes/no): ");
        string needTowing = PromptForYesNo("Does the car need towing / road-side assistance? (yes/no): ");

        // Submit the damage report when carID matches with an existing carID in the car list in car class and other inputs are correct
        try
        {
            DamageReport damageReport = new DamageReport
            {
                UserId = renter.UserId,
                CarId = carId,
                Date = date,
                Time = time,
                Location = location,
                Description = description,
                Car = car, // Set the Car object directly here
                ThirdPartyDamage = thirdPartyDamage,
                ThirdPartyInjury = thirdPartyInjury,
                ScratchOnCar = scratchOnCar,
                NumberOfScratches = numberOfScratches,
                DentOnCar = dentOnCar,
                DentLocation = dentLocation,
                BrokenWindows = brokenWindows,
                BrokenWindowLocation = brokenWindowLocation,
                CarSeverelyDestroyed = carSeverelyDestroyed,
                DestroyedPart = destroyedPart,
                CarBrokeDown = carBrokeDown,
                FlatTire = flatTire,
                NeedTowing = needTowing
            };

            if (damageReport.Car == null)
            {
                throw new Exception("Invalid CarId. Please check the details and try again.");
            }

            // create damage report
            renter.DrList.Add(damageReport);
            Console.WriteLine("\nDamage report submitted successfully.\n");

            // Fetch the insurance details
            Insurance insurance = new Insurance(); // Assuming each car has a default insurance instance
            Insurance carInsurance = insurance.GetInsurance(carId, insurance.CoverageDetails);

            // Calculate total cost
            double totalCost = CalculateRepairCost(damageReport);

            // Insurance coverage (remaining balance calculation)
            double insuranceCoverage = 60000;
            double remainingBalance = totalCost - insuranceCoverage;

            // Display the submitted report
            Console.WriteLine("Submitted Damage Report:");
            damageReport.DisplayReport();

            // Display Schedule the inspection for the car
            car.ScheduleInspection(carId);

            // display insurance coverage
            if (carInsurance != null)
            {
                Console.WriteLine("\nInsurance Details:");
                Console.WriteLine(carInsurance.CoverageDetails);
            }
            else
            {
                Console.WriteLine("No insurance details available for this car.");
            }

            // Display message if towing or roadside assistance is needed
            if (damageReport.NeedTowing.ToLower() == "yes")
            {
                Console.WriteLine("\nWe have sent our tow truck your GPS location. Please wait for our tow truck to come to your assistance.");
            }

            // display message depending on the amount of remaining balance
            if (remainingBalance > 0)
            {
                Console.WriteLine($"\nThis is the Remaining Balance: ${remainingBalance}");
                Console.WriteLine("Please proceed with the payment.");
                // Proceed with payment
                ProcessPayment();
            }
            else
            {
                Console.WriteLine("\nYou're all good. Insurance saved your ass :)");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Please fill in all the details correctly and try again.");
        }
    }



    static double CalculateRepairCost(DamageReport damageReport)
    {
        double totalCost = 0;

        if (damageReport.ScratchOnCar.ToLower() == "yes")
        {
            totalCost += 900 * damageReport.NumberOfScratches; // Cost per scratch
        }

        if (damageReport.DentOnCar.ToLower() == "yes")
        {
            // Count the number of dents
            string[] dents = damageReport.DentLocation.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            totalCost += 850 * dents.Length; // Cost per dent
        }

        if (damageReport.BrokenWindows.ToLower() == "yes")
        {
            // Count the number of broken windows
            string[] windows = damageReport.BrokenWindowLocation.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            totalCost += 1200 * windows.Length; // Cost per window
        }

        if (damageReport.CarSeverelyDestroyed.ToLower() == "yes")
        {
            // Allow for multiple destroyed parts
            string[] destroyedParts = damageReport.DestroyedPart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            totalCost += 50000 * destroyedParts.Length; // Cost per destroyed part
        }
        return totalCost;
    }

    // only called if there is remaining balance to be paid
    static void ProcessPayment()
    {
        string fullName = PromptForString("\nEnter your full name: ");
        string cardNumber = PromptForCardNumber("Enter your card number: ");
        string expirationDate = PromptForExpirationDate("Enter your card's expiration date (mm/yy): ");
        string cvc = PromptForCvc("Enter your card's CVC (3 digits): ");

        Console.WriteLine("\nPayment successful. Thank you!");
    }

    static string PromptForCardNumber(string message)
    {
        string cardNumber;
        while (true)
        {
            cardNumber = PromptForString(message);
            if (cardNumber.Length == 16 && long.TryParse(cardNumber, out _))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid card number. Please enter a valid 16-digit card number.");
            }
        }
        return cardNumber;
    }

    static string PromptForExpirationDate(string message)
    {
        string expirationDate;
        while (true)
        {
            expirationDate = PromptForString(message);
            if (DateTime.TryParseExact(expirationDate, "MM/yy", null, DateTimeStyles.None, out _))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid expiration date. Please enter in MM/yy format.");
            }
        }
        return expirationDate;
    }

    static string PromptForCvc(string message)
    {
        string cvc;
        while (true)
        {
            cvc = PromptForString(message);
            if (cvc.Length == 3 && int.TryParse(cvc, out _))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid CVC. Please enter a 3-digit number.");
            }
        }
        return cvc;
    }

    static string PromptForYesNo(string prompt)
    {
        while (true)
        {
            string input = PromptForString(prompt).ToLower();
            if (input == "yes" || input == "no")
            {
                return input; // Valid yes/no input
            }
            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
        }
    }

    static DateTime PromptForDate(string message)
    {
        Console.Write(message);
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("Invalid date format. Please try again.");
            Console.Write(message);
        }
        return date;
    }

    static TimeSpan PromptForTime(string message)
    {
        Console.Write(message);
        TimeSpan time;
        while (!TimeSpan.TryParse(Console.ReadLine(), out time))
        {
            Console.WriteLine("Invalid time format. Please try again.");
            Console.Write(message);
        }
        return time;
    }



    // zehao end

    //tzi start
    static void RegisterVehicle(CarOwner owner)
    {

        var vehicleDetails = PromptDetails();
        string insurance = PromptInsurance();
        string photos = PromptForPhotos();

        string create = PromptForString("Would you like to add this car to your account?");
        if (create == "Y")
        {
            Car newCar = CreateNewVehicle(vehicleDetails, insurance, owner, photos);
            owner.AddNewListing(newCar);

        }
        else
        {
            Console.WriteLine("Car was not added.");
        }




    }

    static Dictionary<string, object> PromptDetails()
    {
        string make;
        string model;
        int year;
        double mileage;

        while (true)
        {

            make = PromptForString("Enter vehicle make: ");
            model = PromptForString("Enter vehicle model: ");
            year = PromptForInt("Enter year: ");
            mileage = PromptForDouble("Enter mileage: ");


            if (VerifyDetails(make, year, mileage))
            {

                break;
            }
            else
            {
                Console.WriteLine("Invalid vehicle details. Please try again.");
            }
        }

        return new Dictionary<string, object>
    {
        { "Make", make },
        { "Model", model },
        { "Year", year },
        { "Mileage", mileage }
    };
    }

    static bool VerifyDetails(string make, int year, double mileage)
    {

        string[] validMakes = { "Toyota", "Honda", "Ford", "Chevrolet", "BMW" };


        if (Array.IndexOf(validMakes, make) == -1)
        {
            Console.WriteLine("Invalid make. Please enter a valid car brand.");
            return false;
        }


        if (year >= 2024)
        {
            Console.WriteLine("Invalid year. Please enter a year before 2024.");
            return false;
        }


        if (mileage <= 0)
        {
            Console.WriteLine("Invalid mileage. Please enter a valid mileage.");
            return false;
        }

        return true;
    }

    static string PromptInsurance()
    {
        string insurance;
        do
        {
            insurance = PromptForString("Upload insurance details: ");
        } while (!VerifyInsurance(insurance));

        return insurance;
    }

    static bool VerifyInsurance(string insurance)
    {
        return insurance.EndsWith(".txt", StringComparison.OrdinalIgnoreCase);
    }
    static string PromptForPhotos()
    {
        string photos;
        do
        {
            photos = PromptForString("Upload photos: ");
        } while (!VerifyPhotos(photos));

        return photos;
    }

    static bool VerifyPhotos(string photos)
    {
        return photos.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase);
    }

    static Car CreateNewVehicle(Dictionary<string, object> vehicleDetails, string insurance, CarOwner owner, string photos)
    {


        int carId = owner.RegisteredCars.Count + 1;
        string make = (string)vehicleDetails["Make"];
        string model = (string)vehicleDetails["Model"];
        int year = (int)vehicleDetails["Year"];
        double mileage = (double)vehicleDetails["Mileage"];

        return new Car(carId, make, model, year, mileage, insurance, photos);


    }


    static int PromptForInt(string prompt)
    {
        int result;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();


            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty. Please enter a valid integer.");
                continue;
            }


            if (int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
    }


    static string PromptForString(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input; // Valid string input
            }
            Console.WriteLine("Input cannot be empty. Please try again.");
        }
    }

    static double PromptForDouble(string prompt)
    {
        double result;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty. Please enter a valid number.");
            }
            else if (double.TryParse(input, out result))
            {
                return result; // Valid double input
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
    // tzi end
}

