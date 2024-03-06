using System;
using System.IO;
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
        Console.WriteLine("Enter your credit card data:");

        Console.WriteLine("Card number:");
        CreditCardNumber = Console.ReadLine();

        Console.WriteLine("Card expiration date (MM/YY):");
        CardExpirationDate = Console.ReadLine();

        Console.WriteLine("CVV:");
        CardCVV = Console.ReadLine();

        Regex cardNumberRegex = new Regex(@"^\d{16}$");
        Regex expirationDateRegex = new Regex(@"^(0[1-9]|1[0-2])\/\d{2}$");
        Regex cvvRegex = new Regex(@"^\d{3}$");

        if (!cardNumberRegex.IsMatch(CreditCardNumber) ||
            !expirationDateRegex.IsMatch(CardExpirationDate) ||
            !cvvRegex.IsMatch(CardCVV))
        {
            Console.WriteLine("Wrong format.");
            CreditCardNumber = null;
            CardExpirationDate = null;
            CardCVV = null;
        }


    }

    public void SetBalance()
    {
        Console.WriteLine("Enter your balance");
        string balanceInput = Console.ReadLine();

        if (double.TryParse(balanceInput, out double balance))
        {
            Balance = balance;
        }
        else
        {
            Console.WriteLine("Wrong format.");
        }



    }

}
