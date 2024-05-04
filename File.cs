using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class File
{
    Card card = new Card();
    
    string email = ""; 
    string password = "";
    
    string PathToFile = "Accounts.csv";
    string PathToMap = "Accounts";
        
    //Metode, kas ieraksta lietotāja datus csv failā.
    public void Writer(string name, string surname, string email, string phoneNumber, string password)
    {
        string filePath = PathToFile;

        // Atver failu rakstīšanai, papildinot to.
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.Default))
        {
            string line = $"{name},{surname},{email},{phoneNumber},{password}";
            sw.WriteLine(line);
        }
    }

    //Metode, kas izveido kontus csv failos veidā.
    public void CreateAccounts()
    {
        string filePath = PathToFile;
        string filePath2 = PathToMap;

        using (StreamReader sr = new StreamReader(filePath, Encoding.Default)) // Atver failu lasīšanai.
        {
            string line;

            while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
            {
                string[] parts = line.Split(','); // Sadala rindu pa vārdiem.
                string email2 = parts[2]; // Iegūst e-pasta adresi no faila.
                
                string fileName = $"{email2}.csv";
                // Izveido faila ceļu, apvienojot tā nosaukumu un direktoriju.
                string fullFilePath = Path.Combine(filePath2, fileName); 

                FileInfo fileInf = new FileInfo(fullFilePath); // Izveido faila objektu.
                
                if (!fileInf.Exists) // Pārbauda vai fails eksistē un ja neeksistē, izveido to.
                {
                    using (FileStream fs = fileInf.Create()){ }     
                }
            }
        }
    }
    
    //Dzēš kontu(failu) un ieraksta par to csv failā.
    public bool deleteAccount()
    {
        string filePath = PathToFile;
        string fileName = $"{email}.csv";
        string filePath2 = PathToMap;
        string fullPath = Path.Combine(filePath2, fileName); // Izveido faila ceļu, apvienojot tā nosaukumu un direktoriju.

        List<string> listWithoutOneAccount = new List<string>(); 

        Console.WriteLine("Enter your password to confirm your identity: ");
        string inputPassword = Console.ReadLine();

        if (inputPassword == password) // Pārbauda vai ievadītais parole ir pareiza.
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default)) // Atver failu lasīšanai.
            {
                string line;

                while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
                {
                    string[] parts = line.Split(','); // Sadala rindu pa vārdiem.

                    // Pievieno sarakstam kontus, kurus nevajadzētu dzēst.
                    if (!line.Contains(email) && parts[4] != password)
                    {
                        listWithoutOneAccount.Add(line);
                    }
                }
            }
            // Atver failu rakstīšanai un pārraksta to pilnībā.
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default)) 
            {
                foreach (var line in listWithoutOneAccount)
                {
                    sw.WriteLine(line);
                }
            }

            FileInfo fileInf = new FileInfo(fullPath); // Izveido faila objektu.
            fileInf.Delete(); // Dzēš failu.

            Console.WriteLine("\nIt was a pleasure spending time with you. Best wishes!");
            return true;
        }
        else
        {
            Console.WriteLine("Invalid password!");
            return false;
        }
    }
    
    // Metode, kas pārbauda, vai lietotājs ir reģistrējies.
    public void LogIn()
    {
        bool PasswordAndEamilExists = false;

        while (true)
        {
            CreateAccounts();

            string filePath = PathToFile;

            Console.WriteLine("\nTo log in, enter your email and password!\n");
            Console.WriteLine("Enter your email: ");
            
            email = Console.ReadLine();
            
            Console.WriteLine("Enter your password: ");
            
            password = Console.ReadLine();

            using (StreamReader sr = new StreamReader(filePath, Encoding.Default)) // Atver failu lasīšanai.
            {
                string line;

                while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
                {
                  List<string> parts = new List<string>(line.Split(',')); // Sadala rindu pa vārdiem.

                    if (parts.Contains(email)) // Pārbauda vai e-pasts ir sarakstā.
                    {
                        if (parts.Contains(password)) // Pārbauda vai parole ir sarakstā.
                        {
                            PasswordAndEamilExists = true;
                        }
                        else
                        {
                            PasswordAndEamilExists = false;
                        }
                    }
                }
            }
            if (!PasswordAndEamilExists) // Ja parole un e-pasts eksiste, reģistrācija beigsies.
            {
                Console.WriteLine("Invalid email or password. Try again!\n");

                Registration account = new Registration();
                account.Register();
            }
            else
            {
                Console.WriteLine("You are logged in!\n");
                break;
            }
        }
    }

    //Metode, kas ieraksta kartes datus kontu failos.
    public void WriteCardInAccounts()
    {

        string fileName = $"{email}.csv";
        string filePath2 = PathToMap;
        string fullPath = Path.Combine(filePath2, fileName); // Izveido faila ceļu, apvienojot tā nosaukumu un direktoriju.

        string lineToFind = "Card data";
        bool CardExist = false;

        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default)) // Atver failu lasīšanai.
        {
            string line;

            while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
            {
                foreach (string parts in line.Split(';')) // Sadala rindu pa vārdiem.
                {
                    if (parts.Contains(lineToFind)) //Pārbauda, vai karte pastāv.
                    {
                        CardExist = true;
                        Console.WriteLine("\nThe card is already linked!");
                        break;
                    }
                }
            }
        }
        //Ja karte neeksistē, programma liks jums to saistīt
        if (!CardExist)
        {
            card.EnterCreditCardData();
            card.SetBalance();
            
            // Atver failu rakstīšanai, papildinot to.
            using (StreamWriter sw = new StreamWriter(fullPath, true, Encoding.Default)) 
            {
                string line = $"\nCard data;{card.CreditCardNumber};{card.CardExpirationDate};{card.CardCVV};{card.Balance}\n";
                sw.WriteLine(line);
            }
        }
        else
        {
            card.ChangeCardOrBalance(fullPath, lineToFind); //Ja karte eksistē, programma liks jums mainīt kartes datus.
        }
    }
    
    // Izvelk atlikumu no faila, lai jūs varētu veikt pirkumu.
    public void UpdateBalanceFromFile(Card card, Card userCard)
    {
        string fileName = $"{email}.csv";
        string filePath2 = PathToMap;
        string fullPath = Path.Combine(filePath2, fileName); // Izveido faila ceļu, apvienojot tā nosaukumu un direktoriju.

        using (StreamReader sr = new StreamReader(fullPath)) // Atver failu lasīšanai.
        {
            string line;

            while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
            {
                if (line.StartsWith("Card data")) //Meklē rindu ar kartes datiem.
                {
                    string[] parts = line.Split(';'); // Sadala rindu pa vārdiem.
                    double balance;
                    
                    if (double.TryParse(parts[4], out balance)) // Pārbauda vai ir iespējams konvertēt vērtību.
                    {
                        card.Balance = balance;  // Atjauno kartes atlikumu.
                        break; 
                    }
                }
            }
        }
        userCard.Balance = card.Balance; // Atjauno lietotāja konta atlikumu.
    }

    //Metode, kas pēc pirkuma pārraksta mainīto atlikumu failā.
    public void ChangeBalanceAfterPurchase(Card Usercard)
    {
        string fileName = $"{email}.csv";
        string filePath2 = PathToMap;
        string fullPath = Path.Combine(filePath2, fileName); // Izveido faila ceļu, apvienojot tā nosaukumu un direktoriju.

        List<string> lines = new List<string>();

        using (StreamReader sr = new StreamReader(fullPath)) // Atver failu lasīšanai.
        {
            string line;

            while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
            {
                if (line.StartsWith("Card data")) // Meklē rindu ar kartes datiem.
                {
                    string[] parts = line.Split(';'); // Sadala rindu pa vārdiem.
                    parts[4] = Usercard.Balance.ToString(); // Aizstāja atlikumu.
                    line = string.Join(";", parts); // Savieno vārdus atpakaļ.
                    lines.Add(line);
                }
                else
                {
                    lines.Add(line);
                }
            }
        }
        
        // Atver failu rakstīšanai un pārraksta to.
        using (StreamWriter sw = new StreamWriter(fullPath, false, Encoding.Default)) 
        {
            foreach (var line in lines)
            {
                sw.WriteLine(line);
            }
        }
    }

    //Ieraksta preces vajadzīgajā failā, saglabājot kartes datus un nedublējot sarakstus.
    public void WriteShoppingCartInAccounts(List<Product> products)
    {
        string fileName = $"{email}.csv";
        string filePath2 = PathToMap;
        string fullPath = Path.Combine(filePath2, fileName); // Izveido faila ceļu, apvienojot tā nosaukumu un direktoriju.
        
        List<string> existingLines = new List<string>();

        using (StreamReader sr = new StreamReader(fullPath)) // Atver failu lasīšanai.
        {
            string line;

            while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
            {
                //Sarakstai pievieno kartes datus, lai saglabātu tos pārrakstot.
                if (line.StartsWith("Card data"))
                {
                    existingLines.Add(line + "\n");
                }
            }
        }
        
        // Atver failu rakstīšanai un pārraksta pilnībā.
        using (StreamWriter sw = new StreamWriter(fullPath, false, Encoding.Default)) 
        {
            foreach (var line in existingLines)
            {
                sw.WriteLine(line);
            }
            foreach (var prod in products)
            {
                string line = $"Shopping cart;{prod.Name};{prod.Price}";
                sw.WriteLine(line);
            }
        }
    }

    //Ieraksta produktus no vajadzīgā failā bez kartes datiem.
    public void writeShoppingCardInList(List<Product> products)
    {
        string fileName = $"{email}.csv";
        string filePath2 = PathToMap;
        string fullPath = Path.Combine(filePath2, fileName); // Izveido faila ceļu, apvienojot tā nosaukumu un direktoriju.

        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default)) // Atver failu lasīšanai.
        {
            string line;

            while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
            {
                if (line.StartsWith("Shopping cart")) //Pārbauda, vai karte ir ierakstīta failā.
                {
                    string[] parts = line.Split(';'); // Sadaļa rindu pa vārdiem.
                    string productName = parts[1]; // Iegūst produktu nosaukumu.
                    double productPrice;

                    if (double.TryParse(parts[2], out productPrice)) // Pārbauda, vai produkta cena ir skaitlis.
                    {
                        // Izveido produktu objektu ar iegūtajām vērtībām.
                        Product product = new Product(productName, productPrice); 
                        
                        products.Add(product);
                    }
                }
            }
        }
    }

    //Maina paroli ar visām nepieciešamajām prasībām.
    public void changePassword()
    {
        string filePath = PathToFile;
        
        bool passwordChanged = false;
        List<string> Accounts = new List<string>();
        Accounts.Clear(); // Atiestata sarakstu.

        Console.WriteLine("\nEnter your password: ");
        string oldPassword = Console.ReadLine();

        if (oldPassword == password) // Pārbauda vai ievadītais parole ir pareiza.
        {
            Console.WriteLine("Enter your new password: ");
            string newPassword = Console.ReadLine();

            if (!Regex.IsMatch(newPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{8,}$")) // Pārbauda vai parole atbilst prasībām.
            {
                Console.WriteLine("The password must contain at least one uppercase, lowercase letter and number. The password must be at least 8 characters long.");
                return;
            }
            else
            {
                Console.WriteLine("Enter your new password a second time: ");
                string newPassword2 = Console.ReadLine();
                
                if (newPassword == newPassword2) // Pārbauda vai ievadītie paroli ir vienādi.
                {
                    password = newPassword; // Atjauno paroli.
                    Console.WriteLine("Password changed successfully!");
                }
                else
                {
                    Console.WriteLine("New passwords do not match!");
                    return;
                }
            }
        }
        else
        {
            Console.WriteLine("Incorrect password!");
            return;
        }
        
        using (StreamReader sr = new StreamReader(filePath, Encoding.Default)) // Atver failu lasīšanai.
        {
            string line;
 
            while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
            {
                if (line.Contains(email) && line.Contains(oldPassword)) // Pārbauda vai e-pasts un parole ir sarakstā.
                {
                    string[] parts = line.Split(','); // Sadala rindu pa vārdiem.
                    parts[4] = password; // Atjauno paroli.
                    Accounts.Add(string.Join(",", parts)); // Pievieno sarakstam.
                    passwordChanged = true;
                }
                else
                {
                    Accounts.Add(line);
                }
            }
        }
        if (passwordChanged)
        {
            // Atver failu rakstīšanai un pārraksta pilnībā.
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default)) 
            {
                foreach (var part in Accounts)
                {
                    sw.WriteLine(part);
                }
            }
        }
        else
        {
            Console.WriteLine("Incorrect password!");
        }
    } 
}
