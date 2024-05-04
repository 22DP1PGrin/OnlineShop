using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

public class Card
{
    public string CreditCardNumber { get; set; }
    public string CardExpirationDate { get; set; }
    public string CardCVV { get; set; }
    public double Balance { get; set; }

    // Iestata kartes atlikuma sākotnējo vērtību.
    public Card()
    {
        Balance = 0.0;
    }

    // Metode, kas ļauj ievadīt kredītkartes datus.
    public void EnterCreditCardData()
    {
        Regex cardNumberRegex = new Regex(@"^(\d{4}\s){3}\d{4}$"); // kartes numura formāts.
        Regex expirationDateRegex = new Regex(@"^(0[1-9]|1[0-2])\/\d{2}$"); // derīguma termiņa formāts.
        Regex cvvRegex = new Regex(@"^\d{3}$"); // CVV formāts.
        
        Console.WriteLine("\nEnter your credit card data: ");
        Console.WriteLine("\nCard number: ");
        Console.WriteLine("For example: 0000 0000 0000 0000");
        
        CreditCardNumber = Console.ReadLine();

        while (!cardNumberRegex.IsMatch(CreditCardNumber)) // Kamēr kartes numurs nav derīgs.
        {
            Console.WriteLine("Wrong format. Please enter card number again. ");
            Console.WriteLine("\nCard number: ");
            Console.WriteLine("For example: 0000 0000 0000 0000");
            
            CreditCardNumber = Console.ReadLine();
        }
        while (true) 
        {  
            Console.WriteLine("\nCard expiration date (MM/YY): ");
            Console.WriteLine("For example: 12/24");

            CardExpirationDate = Console.ReadLine();
            
            if (!expirationDateRegex.IsMatch(CardExpirationDate)) // Parbauda derīguma termiņa formātu.
            {
                Console.WriteLine("Wrong expiration date format. Please enter the expiration date again. ");
                continue;
            }       
            string[] dateParts = CardExpirationDate.Split('/'); // Atdala derīguma termiņa mēnesi no gada.
            
            int expMonth = int.Parse(dateParts[0]); // Pārveido mēnesi par veselu skaitli.
            int expYear = int.Parse(dateParts[1]); // Pārveido gadu par veselu skaitli.

            if (expYear < 24 || (expYear == 24 && expMonth < 5)) // Pārbauda vai karte ir derīga.
            {
                Console.WriteLine("Your card has expired. Please enter a valid expiration date. "); 
                continue;
            }
            break;
        }
        Console.WriteLine("\nCVV:");
        Console.WriteLine("For example: 000");
        
        CardCVV = Console.ReadLine();

        while (!cvvRegex.IsMatch(CardCVV)) // Kamēr CVV nav derīgs.
        {
            Console.WriteLine("Wrong format for CVV. Please enter the CVV again. ");
            Console.WriteLine("\nCVV:");
            Console.WriteLine("For example: 000");
    
            CardCVV = Console.ReadLine();
        }
    }

    // Metode, kas ļauj ievadīt kredītkartes atlikumu.
    public void SetBalance()
    {
        while (true)
        {
            Console.WriteLine("\nEnter your balance");
            string balanceInput = Console.ReadLine();
            
            balanceInput = balanceInput.Replace(',', '.'); // Izņem komatu, ja tas ir.

            if (!double.TryParse(balanceInput, out double balance)) // Pārbauda vai ievadītais atlikums ir derīgs skaitlis.
            {
                Console.WriteLine("Wrong format! Please enter your balance again.");
            }
            else
            {
                Balance=balance; // Iestata jaunu atlikumu.
                break;
            }
        }
    }
    
    // Metode, kas ļauj mainīt kredītkartes informāciju vai bilanci un ierakstīt jauno informāciju attiecīgajā failā.
    public void ChangeCardOrBalance(string FileName, string lineToFind)
    {
        List<string> lines = new List<string>();  // Izveido sarakstu, kurā glabās visas rindas no failā.

        Console.WriteLine("Do you want to change your card and/or its balance?(Y/N)");
        string choice=Console.ReadLine();
        
        if (choice.ToLower() == "y")
        {
            // Izsauc metodes, lai mainītu kartes datus.
            EnterCreditCardData();
            SetBalance();
            
            using (StreamReader sr = new StreamReader(FileName, Encoding.Default)) // Atver failu lasīšanai.
            {
                string line;
                
                while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
                {
                    if (line.Contains(lineToFind)) // Pārbauda vai rinda satur ievadīto tekstu.
                    {
                        // Pievieno sarakstam mainītos datus.
                        lines.Add("\nCard data;" + CreditCardNumber + ";" + CardExpirationDate + ";" + CardCVV + ";" + Balance);
                    }
                    else
                    {
                        lines.Add(line);  //Pievieno sarakstam visu, kas saistīts ar produktiem grozā.
                    }
                }
            }
            // Atver failu rakstīšanai un pārraksta to pilnībā.
            using (StreamWriter writer = new StreamWriter(FileName,false, Encoding.Default)) 
            {
                foreach (string line in lines) 
                {
                    writer.WriteLine(line); // Ieraksta visas vērtības, kas ir sarakstā.
                }
            }
        }   
    }
}
