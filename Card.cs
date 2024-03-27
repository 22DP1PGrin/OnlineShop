using System;
using System.Text.RegularExpressions;

public class Card
{
    public string CreditCardNumber { get; set; }
    public string CardExpirationDate { get; set; }
    public string CardCVV { get; set; }
    public double Balance { get; set; }
    public bool CardDataEntered { get; set; }

    public Card()
    {
        Balance = 0.0;
        CardDataEntered = false;
    }

    public void EnterCreditCardData()
    {
        Regex cardNumberRegex = new Regex(@"^(\d{4}\s){3}\d{4}$");
        Regex expirationDateRegex = new Regex(@"^(0[1-9]|1[0-2])\/\d{2}$");
        Regex cvvRegex = new Regex(@"^\d{3}$");
        Console.WriteLine("Enter your credit card data:");

        Console.WriteLine("Card number:");
        Console.WriteLine("For example: 0000 0000 0000 0000");
        CreditCardNumber = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("Card expiration date (MM/YY):");
            Console.WriteLine("For example: 12/24");
            CardExpirationDate = Console.ReadLine();

            if (!expirationDateRegex.IsMatch(CardExpirationDate))
            {
                Console.WriteLine("Wrong expiration date format. Please enter the expiration date again:");
                continue;
            }

            string[] dateParts = CardExpirationDate.Split('/');
            int expMonth = int.Parse(dateParts[0]);
            int expYear = int.Parse(dateParts[1]);

            if (expYear < 24 || (expYear == 24 && expMonth < 5))
            {
                Console.WriteLine("Your card has expired. Please enter a valid expiration date:");
                continue;
            }

            break;
        }

        Console.WriteLine("CVV:");
        Console.WriteLine("For example: 000");
        CardCVV = Console.ReadLine();

        while (!cvvRegex.IsMatch(CardCVV))
        {
            Console.WriteLine("Wrong format for CVV. Please enter the CVV again:");
            CardCVV = Console.ReadLine();
        }
    }

    public void SetBalance()
    {
        while (true)
        {
            Console.WriteLine("Enter your balance");
            string balanceInput = Console.ReadLine();

            if (!double.TryParse(balanceInput, out double balance))
            {
                Console.WriteLine("Wrong format! Please enter your balance again:");
            }
            else
            {
                Balance = balance;
                break;
            }
        }
    }
}

