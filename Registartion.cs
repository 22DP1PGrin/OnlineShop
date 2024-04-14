using System;
using System.Text.RegularExpressions;

public class Registration
{
    UserTest user = new UserTest();
    public void Register()
    {
        Console.WriteLine("Welcome to the store \"Name\". To continue working, you must log in to your account.\nDon't have an account? Register!\n");
        
        while (true)
        {
            Console.WriteLine("Enter \"Sign in\" to register or \"Log in\" to log in.");
            string Choice = Console.ReadLine();
            
            if (Choice.ToLower() == "sign in")
            {
                user.name();
                user.surname();
                user.email();
                user.phoneNumber();
                user.password();
                user.password2();
                
                Console.WriteLine("You have successfully registered!");
                break;
            }
            else
            {
                if (Choice.ToLower() == "log in")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again!");
                }
            }
        }
    }
}
