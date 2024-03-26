uusing System;
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
        Regex cardNumberRegex = new Regex(@"^(\d{4}\s){3}\d{4}$");
        Regex expirationDateRegex = new Regex(@"^(0[1-9]|1[0-2])\/\d{2}$");
        Regex cvvRegex = new Regex(@"^\d{3}$");
        Console.WriteLine("Enter your credit card data:");


        while (true)
        {
            Console.WriteLine("Card number:");
            Console.WriteLine("For example: 0000 0000 0000 0000");
            CreditCardNumber = Console.ReadLine();

            Console.WriteLine("Card expiration date (MM/YY):");
            Console.WriteLine("For example: 12/24");
            CardExpirationDate = Console.ReadLine();

            Console.WriteLine("CVV:");
            Console.WriteLine("For example: 000");
            CardCVV = Console.ReadLine();
             if (!cardNumberRegex.IsMatch(CreditCardNumber) || !expirationDateRegex.IsMatch(CardExpirationDate) || !cvvRegex.IsMatch(CardCVV))
             {
                Console.WriteLine("Wrong format. Try again!");
               
             }
             else
             {
                break;
             }
        }
    }

    public void SetBalance()
    {

        while (true)
        {
            Console.WriteLine("Enter your balance");
            string balanceInput = Console.ReadLine();

            if (double.TryParse(balanceInput, out double balance))
            {
                Console.WriteLine("Wrong format!");
            }
            else
            {
                Balance = balance;
                break;
            }
        }
    }
}

}
