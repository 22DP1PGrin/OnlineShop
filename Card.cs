using System;
using System.Text;
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

    // Metode, kas ļauj ievadīt kredītkartes datus.
    public void EnterCreditCardData()
    {
        Regex cardNumberRegex = new Regex(@"^(\d{4}\s){3}\d{4}$"); // kartes numura formāts
        Regex expirationDateRegex = new Regex(@"^(0[1-9]|1[0-2])\/\d{2}$"); // derīguma termiņa formāts
        Regex cvvRegex = new Regex(@"^\d{3}$"); // CVV formāts
        
        Console.WriteLine("\nEnter your credit card data: ");
        Console.WriteLine("Card number: ");
        Console.WriteLine("For example: 0000 0000 0000 0000.");
        CreditCardNumber = Console.ReadLine();

        while (!cardNumberRegex.IsMatch(CreditCardNumber))
        {
            Console.WriteLine("Wrong format. Please enter card number again: ");
            CreditCardNumber = Console.ReadLine();
        }
        while (true)
        {
            Console.WriteLine("Card expiration date (MM/YY): ");
            Console.WriteLine("For example: 12/24.");
            CardExpirationDate = Console.ReadLine();

            if (!expirationDateRegex.IsMatch(CardExpirationDate))
            {
                Console.WriteLine("Wrong expiration date format. Please enter the expiration date again: ");
                continue;
            }

            string[] dateParts = CardExpirationDate.Split('/'); // atdala derīguma termiņa mēnesi no gada
            int expMonth = int.Parse(dateParts[0]);
            int expYear = int.Parse(dateParts[1]);

            if (expYear < 24 || (expYear == 24 && expMonth < 5)) // pārbauda vai karte ir derīga
            {
                Console.WriteLine("Your card has expired. Please enter a valid expiration date: "); // ja karte nav derīga, ir jāievada derīguma tērmiņu vēlreiz, kamēr nebūs pareizs
                continue;
            }
            break;
        }

        Console.WriteLine("CVV:");
        Console.WriteLine("For example: 000.");
        CardCVV = Console.ReadLine();

        while (!cvvRegex.IsMatch(CardCVV))
        {
            Console.WriteLine("Wrong format for CVV. Please enter the CVV again: ");
            CardCVV = Console.ReadLine();
        }
    }

    // Metode, kas ļauj ievadīt kredītkartes atlikumu.
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
                break;
            }
        }
    }
    
    // Metode, kas ļauj mainīt kredītkartes datus vai atlikumu.
    public void ChangeCardOrBalance(string FileName, string lineToFind)
    {
        List<string> lines = new List<string>();

        Console.WriteLine("Do you want to change your card and/or its balance?(Y/N)");
        string choice=Console.ReadLine();
        if (choice.ToLower() == "y")
        {
            EnterCreditCardData();
            SetBalance();
            using (StreamReader sr = new StreamReader(FileName, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(lineToFind))
                    {
                        lines.Add("Card data;" + CreditCardNumber + ";" + CardExpirationDate + ";" + CardCVV + ";" + Balance);
                    }
                    else
                    {
                        lines.Add(line);
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }   
    }
}
