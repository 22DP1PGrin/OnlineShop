using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
public class File
{
    string email = "";
    string password = "";

    Card card = new Card();

    //Metode, kas atrod ceļu uz failu Accounts.csv.
    public string pathToAccountsCsv()
    {
        string filePath = "Accounts.csv";
        return filePath;
    }
    
    //Metode, kas atrod ceļu uz mapes Accounts.
    public string pathToAccountsMap()
    {
        string filePath = "Accounts";
        return filePath;
    }

    //Metode, kas ieraksta lietotāja datus csv failā.
    public void Writer(string name, string surname, string email, string phoneNumber, string password)
    {
        string filePath = pathToAccountsCsv();

        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.Default))
        {
            string line = $"{name},{surname},{email},{phoneNumber},{password}";
            sw.WriteLine(line);
        }
    }

    //Metode, kas izveido kontus csv failos veidā.
    public void CreateAccounts()
    {
        string filePath = pathToAccountsCsv();
        string filePath2 = pathToAccountsMap();

        using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string email2 = parts[2];
                string fileName = $"{email2}.csv";
                string fullFilePath = Path.Combine(filePath2, fileName);

                FileInfo fileInf = new FileInfo(fullFilePath);
                if (!fileInf.Exists)
                {
                    using (FileStream fs = fileInf.Create()){ }     
                }
            }
        }
    }
    
    // Metode, kas pārbauda, vai lietotājs ir reģistrējies.
    public void LogIn()
    {
        bool PasswordExists = false;

        while (true)
        {
            CreateAccounts();

            string filePath = pathToAccountsCsv();

            Console.WriteLine("\nTo log in, enter your email and password!\n");
            Console.WriteLine("Enter your email: ");
            email = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            password = Console.ReadLine();

            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                  List<string> parts = new List<string>(line.Split(','));

                    if (parts.Contains(email))
                    {
                        if (parts.Contains(password))
                        {
                            PasswordExists = true;
                        }
                        else
                        {
                            PasswordExists = false;
                        }
                    }
                }
            }
            if (!PasswordExists)
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
        string filePath2 = pathToAccountsMap();
        string fullPath = Path.Combine(filePath2, fileName);

        string lineToFind = "Card data";
        bool CardExist = false;

        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                foreach (string parts in line.Split(';'))
                {
                    if (parts.Contains(lineToFind))
                    {
                        CardExist = true;
                        Console.WriteLine("\nThe card is already linked!");
                        break;
                    }
                }
            }
        }
        if (!CardExist)
        {
            card.EnterCreditCardData();
            card.SetBalance();
            
            using (StreamWriter sw = new StreamWriter(fullPath, true, Encoding.Default))
            {
                string line = $"Card data;{card.CreditCardNumber};{card.CardExpirationDate};{card.CardCVV};{Balance}\n";
                sw.WriteLine(line);
            }
        }
        else
        {
            card.ChangeCardOrBalance(fullPath, lineToFind);
        }
    }

    //Ieraksta preces vajadzīgajā failā, saglabājot kartes datus un nedublējot sarakstus.
    public void WriteShoppingCartInAccounts(List<Product> products)
    {
        string fileName = $"{email}.csv";
        string filePath2 = pathToAccountsMap();
        string fullPath = Path.Combine(filePath2, fileName);
        
        List<string> existingLines = new List<string>();

        using (StreamReader sr = new StreamReader(fullPath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Card data"))
                {
                    existingLines.Add(line + "\n");
                }
            }
        }
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
        string filePath2 = pathToAccountsMap();
        string fullPath = Path.Combine(filePath2, fileName);

        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Shopping cart"))
                {
                    string[] parts = line.Split(';');
                    string productName = parts[1];
                    double productPrice;

                    if (double.TryParse(parts[2], out productPrice))
                    {
                        Product product = new Product(productName, productPrice);
                        products.Add(product);
                    }
                }
            }
        }
    }

    public double Balance()
    {
        string fileNmae = $"{email}.csv";
        string filePath2 = pathToAccountsMap();
        string fullPath = Path.Combine(filePath2, fileNmae);
        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Card data"))
                {
                    string[] parts = line.Split(';');
                    double balance;
                    if (double.TryParse(parts[3], out balance))
                    {
                        return balance;
                    }
                }

            }
        }
        return 0.0;
    }

    //Dzēš kontu(failu) un ieraksta par to csv failā.
    public bool deleteAccount()
    {
        string filePath = pathToAccountsCsv();
        string fileName = $"{email}.csv";
        string filePath2 = pathToAccountsMap();
        string fullPath = Path.Combine(filePath2, fileName);

        List<string> listWithoutAccount = new List<string>();

        Console.WriteLine("Enter your password to confirm your identity: ");
        string inputPassword = Console.ReadLine();

        if (inputPassword == password)
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    
                    if (!line.Contains(email) && parts[4] != password)
                    {
                        listWithoutAccount.Add(line);
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                foreach (var line in listWithoutAccount)
                {
                    sw.WriteLine(line);
                }
            }

            FileInfo fileInf = new FileInfo(fullPath);
            fileInf.Delete();

            Console.WriteLine("\nIt was a pleasure spending time with you. Best wishes!");
            return true;
        }
        else
        {
            Console.WriteLine("Invalid password!");
            return false;
        }
    }

    //Maina paroli ar visām nepieciešamajām prasībām.
    public void changePassword()
    {
        string filePath = pathToAccountsCsv();
        
        bool passwordChanged = false;
        List<string> Accounts = new List<string>();
        Accounts.Clear();

        Console.WriteLine("\nEnter your password: ");
        string oldPassword = Console.ReadLine();

        if (oldPassword == password)
        {
            Console.WriteLine("Enter your new password: ");
            string newPassword = Console.ReadLine();

            if (!Regex.IsMatch(newPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{8,}$"))
            {
                Console.WriteLine("The password must contain at least one uppercase, lowercase letter and number. The password must be at least 8 characters long.");
                return;
            }
            else
            {
                Console.WriteLine("Enter your new password a second time: ");
                string newPassword2 = Console.ReadLine();
                
                if (newPassword == newPassword2)
                {
                    password = newPassword;
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
        using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
        {
            string line;
 
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(email) && line.Contains(oldPassword))
                {
                    string[] parts = line.Split(',');
                    parts[4] = password;
                    Accounts.Add(string.Join(",", parts));
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
