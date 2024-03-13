using System.IO;

public class File
{
    public void Writer(string Name, string Surname, string PhoneNumber, string Email, string Password)
    {
        string path = @"C:\Users\pgrin\source\repos\ConsoleApp3\ConsoleApp3\Accounts.csv";
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                string line = $"{Name},{Surname},{PhoneNumber},{Email},{Password}";
                sw.WriteLine(line);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void ReaderForLogin()
    {
        Console.WriteLine("Enter your email!");
        string email = Console.ReadLine();
        string path = @"C:\Users\pgrin\source\repos\ConsoleApp3\ConsoleApp3\Accounts.csv";
        
        try
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                bool emailExists = false;
                while ((line = sr.ReadLine()) != null)
                {

                    string[] parts = line.Split(',');
                    
                    if(parts.Contains(email))
                    {

                        Console.WriteLine("Enter your password!");
                        string password = Console.ReadLine();
                        if (parts.Contains(password))
                        {
                            //Console.WriteLine("You are logged in!");
                            emailExists = true;
                            
                        }
                        else
                        {
                            //Console.WriteLine("Invalid password!");
                            emailExists = true;
                        }
                    }
                    
                }
                if (!emailExists)
                {
                    Console.WriteLine("Invalid password or email. Try again.");
                }
                else
                {
                    
                    Console.WriteLine("You are logged in!");
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
