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
    public void Reader()
    {
        Console.WriteLine("Enter your email!");
        string email = Console.ReadLine();
        string path = @"C:\Users\pgrin\source\repos\ConsoleApp3\ConsoleApp3\Accounts.csv";
        try
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts=line.Split(',');
                    if (parts[4]==email)
                    {
                        Console.WriteLine("Enter your password!");
                        string password = Console.ReadLine();
                        if (parts[5]==password)
                        {
                            Console.WriteLine("You are logged in!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid password! Try again");
                        }
                    
                    }
                    else
                    {
                        Console.WriteLine("Invalid email! Try again");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
