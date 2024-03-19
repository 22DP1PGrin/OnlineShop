uusing System;

public class File
{
    public void Writer(string Name, string Surname, string PhoneNumber, string Email, string Password)
    {
        string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";
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
        string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";
        
        try
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                bool paroleExists = false;
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
                            paroleExists = true;
                            
                        }
                        else
                        {
                            //Console.WriteLine("Invalid password!");
                            paroleExists = false;
                        }
                    }
                    
                }


                if (!paroleExists)
                {
                    Console.WriteLine("Invalid email or password. Try again!");
                    Registration obj = new Registration();
                    obj.Method1();
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
