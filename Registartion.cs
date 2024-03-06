using System;
using System.Text.RegularExpressions;

public class Registration
{
    public void Method1()
    {
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
                    Console.WriteLine("Enter your email: ");
                    Email = Console.ReadLine();
                    if (!Regex.IsMatch(Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        Console.WriteLine("Invalid email address. Try again!");
                    }
                } while (!Regex.IsMatch(Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"));

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
                break;
            }
            else
            {
                File File = new File();
                File.Reader();

            }
        }
    }
}
