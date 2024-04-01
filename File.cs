using System;
using System.Text;
using System.IO;
public class File
{
    string email = "";
    bool PasswordExists = false;

    //string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";
    string path = @"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts.csv";
    
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
                string fullFilePath = Path.Combine(@"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts\", fileName);
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
    public void WriteCardInAccounts(string Number, string ExpirationDate, string CVV, double Balance)
    {

        string fileName = $"{email}.csv";
        string fullPath = Path.Combine(@"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts", fileName);

        try
        {
            using (StreamWriter sw = new StreamWriter(fullPath, true, System.Text.Encoding.Default))
            {
                string line = $"{Number},{ExpirationDate},{CVV},balance:{Balance}";
                sw.WriteLine("Card data: "+line+"\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
