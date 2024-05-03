using System;
using System.IO;
using System.Text.RegularExpressions;

public class UserTest
{
    string Name, Surname, PhoneNumber, Email, Password, Password2;
    string[] userData = new string[5];

    File file = new File();
    
    //Pārbauda lietotājvārda rakstzīmes, izņemot burtus, un neiziet no сikla, kamēr vards nav pareizs.
    public void name()
    {
        do
        {
            Console.WriteLine("Enter your name: ");
            Name = Console.ReadLine();
            
            if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Invalid name. Try again!");
            }
            else
            {
                userData[0] = Name;
                break;
            }
        } while (true);
    }
    
    //Pārbauda lietotājuzvārda rakstzīmes, izņemot burtus, un neiziet no сikla, kamēr uzvards nav pareizs.
    public void surname()
    {
        do
        {
            Console.WriteLine("Enter your surname: ");
            Surname = Console.ReadLine();
            
            if (!Regex.IsMatch(Surname, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Invalid surname. Try again!");
            }
            else
            {
                userData[1] = Surname;
                break;
            }
        } while (true);
    }
    
    //Pārbauda lietotāja e-pasta adresi, lai tā atbilstu e-pasta formātam un neiziet no сikla, kamēr e-pasta nav pareiza.
    //Ja failā ir tas pats e-pasts, tas piedāvās iziet no programmas vai piedāvās vēlreiz ievadīt.

    public void email()
    {
        while (true)
        {
            string fileName=file.pathToAccountsCsv();
            bool emailExists = false;
            
            Console.WriteLine("Enter your email: ");
            Email = Console.ReadLine();
            
            using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
            {
                string line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    
                    if (parts.Length > 3 && parts[2] == Email)
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
                userData[2] = Email;
                break;
            }
            else
            {
                Console.WriteLine("Invalid email address. Try again!");
            }
        }
    }
    //Pārbauda lietotāja telefona numuru, lai tas atbilstu telefona numura formātam un neiziet no сikla, kamēr numurs nav pareizs.
    public void phoneNumber()
    {
        do
        {
            Console.WriteLine("Enter your phone number: ");
            PhoneNumber = Console.ReadLine();
            
            if (!Regex.IsMatch(PhoneNumber, @"^\+\d+$"))
            {
                Console.WriteLine("Invalid phone number. Try again!");
            }
            else
            {
                userData[3] = PhoneNumber;
                break;
            }
        } while (true);
    }

    //Pārbauda lietotāja paroli un neiziet no сikla, kamēr vards nav pareizs.
    //Tajā būtu vismaz viens lielais burt, mazais burts un cipars. Parolei jābūt vismaz 8 rakstzīmēm garai. 
    public void password()
    {
        do
        {
            Console.WriteLine("Enter your password: ");
            Password = Console.ReadLine();
            
            if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{8,}$"))
            {
                Console.WriteLine("The password must contain at least one uppercase, lowercase letter and number. The password must be at least 8 characters long.");
            }
            else
            {
                break;
            }
        } while (true);

    }

    //Pārbauda, vai lietotāja ievadītā parole sakrīt ar iepriekš ievadīto paroli un neiziet no сikla, kamēr otra parole nav pareiza.
    public void password2()
    {
        do
        {
            Console.WriteLine("Enter your password again: ");
            Password2 = Console.ReadLine();
            
            if (Password != Password2)
            {
                Console.WriteLine("Passwords do not match. Try again!");
            }
            else
            {
                userData[4] = Password2;
                file.Writer(userData[0], userData[1], userData[2], userData[3], userData[4]);
                break;
            }
        } while (true);
    }
}
