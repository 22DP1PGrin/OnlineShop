using System.IO;

public class LogIn
{
    string email = "";
    bool PasswordExists = false;

    string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";
    //string path = @"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts.csv";

    public void Log()
    {
        while(true)
        {
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
                    Console.WriteLine("Invalid email or password. Try again!");
                    Registration obj = new Registration();
                    obj.Register();
                }
                else
                {

                    Console.WriteLine("You are logged in!");
                    break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
