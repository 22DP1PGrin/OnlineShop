using System;
using System.Text;
using System.IO;
public class File
{
    string email = "";
    
    Card card = new Card();

    //Metode, kas atrod ceļu uz failu Accounts.csv
    public string pathToAccountsCsv()
    {
        //Iegūstiet darba direktoriju
        string currentDirectory = Directory.GetCurrentDirectory();
        
        // Paceļas par 2 līmeņiem uz augšu, lai sasniegt projekta saknes direktoriju.
        string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
       
        //Ģenerē pilnu ceļu uz failu Accounts.csv projekta saknē
        string filePath = Path.Combine(projectDirectory, "Accounts.csv");
        
        return filePath;
    }
    public string pathToAccountsMap()
    {
        //Atrod darba direktoriju
        string currentDirectory = Directory.GetCurrentDirectory();
        // Paceļas par 3 līmeņiem uz augšu, lai sasniegtu projekta saknes direktoriju
        string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
        //Ģenerē pilnu ceļu uz failu
        string filePath = Path.Combine(projectDirectory, "Accounts");
        
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
                    using (FileStream fs = fileInf.Create())
                    {
                    }
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
            Console.WriteLine("\nTo log in, enter your email and password!\n");
            Console.WriteLine("Enter your email! ");
            email = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            string password = Console.ReadLine();

            string filePath = pathToAccountsCsv();

            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    
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
                
                Registration obj = new Registration();
                obj.Register();
            }
            else
            {
                Console.WriteLine("You are logged in!\n");
                break;
            }
        }
    }

    //Metode, kas ieraksta kartes datus kontu failos.
    public void WriteCardInAccounts(string Number, string ExpirationDate, string CVV, double Balance)
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
                        Console.WriteLine("The card is already linked!");
                        break;
                    }
                }
            }
        }
        if (!CardExist)
        {
            using (StreamWriter sw = new StreamWriter(fullPath, true, Encoding.Default))
            {
                string line = $"Card data;{Number};{ExpirationDate};{CVV};{Balance}\n";
                sw.WriteLine(line);
            }
            card.Balance = Balance;
        }
        else
        {
            card.ChangeCardOrBalance(fullPath, lineToFind);
        }
    }

    //Ieraksta preces vajadzīgajā failā, saglabājot kartes datus un nedublējot sarakstus
    public void WriteShoppingCartInAccounts(List<Product> products)
    {
        string fileName = $"{email}.csv";
        string filePath2 = pathToAccountsMap();
        string fullPath = Path.Combine(filePath2, fileName);
        List<string> existingLines = new List<string>();
        
        using (StreamReader sr = new StreamReader(fullPath))
        {
            string line;
            List<string> updatedLines = new List<string>();
            
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Card data"))
                {
                    existingLines.Add(line+"\n");
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

    //Ieraksta produktus no vajadzīgā failā bez kartes datiem
    public void writeShoppingCardInList(List<Product> products)
    {
        string fileName=$"{email}.csv";
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
    public void Balance()
    {
        string fileNmae = $"{email}.csv";
        string filePath2 = pathToAccountsMap();
        string fullPath = Path.Combine(filePath2, fileNmae);
        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if(line.StartsWith("Card data"))
                {
                    string[] parts = line.Split(';');
                    double balance;
                    if (double.TryParse(parts[3], out balance))
                    {
                        double newBalance = balance;
                        Console.WriteLine(newBalance);
                    }
                }

            }
        }
    }
}
