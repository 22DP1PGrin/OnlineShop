using System;
using System.Text.RegularExpressions;

public class Registration
{
    public void Method1()
    {
        string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";
        //string path = @"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts.csv";
        string Name, Surname, PhoneNumber, Email, Password, Password2;

        Console.WriteLine("Welcome to the store \"Name\". To continue working, you must log in to your account.");
        Console.WriteLine("Don't have an account? Register!");
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("Enter \"Sign in\" to register or \"Log in\" to log in.");
            string Choice = Console.ReadLine();
            if (Choice.ToLower() == "sign in")
            {
                do
                {
                    Console.WriteLine("Enter your name: ");
                    Name = Console.ReadLine();
                    if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
                    {
                        Console.WriteLine("Invalid name. Try again!");
                    }
                } while (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"));

                do
                {
                    Console.WriteLine("Enter your surname: ");
                    Surname = Console.ReadLine();
                    if (!Regex.IsMatch(Surname, @"^[a-zA-Z]+$"))
                    {
                        Console.WriteLine("Invalid surname. Try again!");
                    }
                } while (!Regex.IsMatch(Surname, @"^[a-zA-Z]+$"));
                while (true)
                {
                    bool emailExists = false;
                    Console.WriteLine("Enter your email: ");
                    Email = Console.ReadLine();
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length > 3 && parts[3] == Email)
                            {
                                Console.WriteLine("This email is already in use. \r\nDo you want to cancel your registration?");
                                string choice = Console.ReadLine();
                                if (choice.ToLower() == "yes")
                                {
                                    return;
                                }
                                emailExists = true;
                                break;
                            }
                        }
                    }

                    if (!emailExists && Regex.IsMatch(Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid email address. Try again!");
                    }
                }
                do
                {
                    Console.WriteLine("Enter your phone number: ");
                    PhoneNumber = Console.ReadLine();
                    if (!Regex.IsMatch(PhoneNumber, @"^\+\d+$"))
                    {
                        Console.WriteLine("Invalid phone number. Try again!");
                    }
                } while (!Regex.IsMatch(PhoneNumber, @"^\+\d+$"));
                do
                {
                    Console.WriteLine("Enter your password: ");
                    Password = Console.ReadLine();
                    if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{8,}$"))
                    {
                        Console.WriteLine("The password must contain at least one uppercase, lowercase letter and number. The password must be at least 8 characters long.");
                    }
                } while (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{8,}$"));

                do
                {
                    Console.WriteLine("Enter your password again: ");
                    Password2 = Console.ReadLine();
                    if (Password != Password2)
                    {
                        Console.WriteLine("Passwords do not match. Try again!");
                    }
                } while (Password != Password2);

                Console.WriteLine("You have successfully registered!");
                File file = new File();
                file.Writer(Name, Surname, PhoneNumber, Email, Password);
                file.Accounts();
            }
            else { 
                if(Choice.ToLower() == "log in")
                {
                    File File = new File();
                    File.ReaderForLogin();
                    File.Accounts();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command. Try again!");
                }

            }
        }
    }
}
