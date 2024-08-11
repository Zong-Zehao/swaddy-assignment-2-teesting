using System.Globalization;

public class Payment
{
    public string FullName { get; private set; }
    public string CardNumber { get; private set; }
    public string CardCVC { get; private set; }
    public string CardExpirationDate { get; private set; }

    public void ProcessPayment()
    {
        FullName = PromptForString("\nEnter your full name: ");
        CardNumber = PromptForCardNumber("Enter your card number: ");
        CardExpirationDate = PromptForExpirationDate("Enter your card's expiration date (mm/yy): ");
        CardCVC = PromptForCvc("Enter your card's CVC (3 digits): ");

        Console.WriteLine("\nPayment successful. Thank you!");
    }

    private string PromptForCardNumber(string message)
    {
        string cardNumber;
        while (true)
        {
            cardNumber = PromptForString(message);
            if (cardNumber.Length == 16 && long.TryParse(cardNumber, out _))
            {
                return cardNumber;
            }
            else
            {
                Console.WriteLine("Invalid card number. Please enter a valid 16-digit card number.");
            }
        }
    }

    private string PromptForExpirationDate(string message)
    {
        string expirationDate;
        while (true)
        {
            expirationDate = PromptForString(message);
            if (DateTime.TryParseExact(expirationDate, "MM/yy", null, DateTimeStyles.None, out _))
            {
                return expirationDate;
            }
            else
            {
                Console.WriteLine("Invalid expiration date. Please enter in MM/yy format.");
            }
        }
    }

    private string PromptForCvc(string message)
    {
        string cvc;
        while (true)
        {
            cvc = PromptForString(message);
            if (cvc.Length == 3 && int.TryParse(cvc, out _))
            {
                return cvc;
            }
            else
            {
                Console.WriteLine("Invalid CVC. Please enter a 3-digit number.");
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
}