using System;
using System.Text;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
public class File
{
    string email = "";
    bool PasswordExists = false;

    Card card=new Card();

    string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";
    //string path = @"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts.csv";
    
    //Metode, kas ieraksta lietotāja datus csv failā.
    public void Writer(string name, string surname, string email, string phoneNumber, string password)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                string line = $"{name},{surname},{email},{phoneNumber},{password}";
                sw.WriteLine(line);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    //Metode, kas izveido kontus csv failos.
    public void CreateAccounts()
    {
        using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string email2 = parts[3];
                string fileName = $"{email2}.csv";
                string fullFilePath = Path.Combine(@"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts\", fileName);
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
        while (true)
        {
            Console.WriteLine("To log in, enter your mail and email!\n");
            Console.WriteLine("Enter your email! ");
            email = Console.ReadLine();
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Contains(email))
                        {
                            Console.WriteLine("Enter your password: ");
                            string password = Console.ReadLine();
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    
    //Metode, kas ieraksta kartes datus kontu failos.
    public void WriteCardInAccounts(string Number, string ExpirationDate, string CVV, double Balance)
    {

        string fileName = $"{email}.csv";
        string fullPath = Path.Combine(@"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts\", fileName);
        string lineToFind = "Card data";
        bool CardExist = false;

        using (StreamReader sr = new StreamReader(fullPath, Encoding.Default))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                foreach (string parts in line.Split(','))
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
            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true, Encoding.Default))
                {
                    string line = $"Card data,{Number},{ExpirationDate},{CVV},{Balance}\n";
                    sw.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
        {
            card.ChangeCardOrBalance(fullPath, lineToFind);
        }
    }
}
