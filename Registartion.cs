using System;

public class Registration
{
    UserTest user = new UserTest();
    
    // Metode, kas ļauj lietotājam reģistrēties.
    public void Register()
    {
        Console.WriteLine("Welcome to the store \"The Chuckle Cart\". To continue working, you must log in to your account.\nDon't have an account? Register!");

        while (true)
        {
            Console.WriteLine("\nEnter \"Sign in\" to register or \"Log in\" to log in.");
            
            string Choice = Console.ReadLine();

            if (Choice.ToLower() == "sign in")
            {
                // Izsauc metodes, kurās tiek ievadīti un pārbaudīti nepieciešamie dati.
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
