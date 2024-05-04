using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class UserTest
{
    File file=new File();
    
    string Name, Surname, PhoneNumber, Email, Password, Password2;
    string[] userData = new string[5]; // Masīvs, kurā tiks glabāti dati no lietotāja ievades.

    string pathToFile = "Accounts.csv"; 
    
    //Pārbauda lietotājvārda rakstzīmes, izņemot burtus, un neiziet no сikla, kamēr vards nav pareizs.
    public void name()
    {
        do
        {
            Console.WriteLine("\nEnter your name: ");
            Name = Console.ReadLine();
            
            if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$")) // Pārbauda vai vārds sastāv tikai no burtiem.
            {
                Console.WriteLine("Invalid name. Try again!");
            }
            else
            {
                userData[0] = Name; // Ievieto vārdu masīvā.
                break;
            }
        } while (true);
    }
    
    //Pārbauda lietotājuzvārda rakstzīmes, izņemot burtus, un neiziet no сikla, kamēr uzvards nav pareizs.
    public void surname()
    {
        do
        {
            Console.WriteLine("\nEnter your surname: ");
            Surname = Console.ReadLine();
            
            if (!Regex.IsMatch(Surname, @"^[a-zA-Z]+$")) // Pārbauda vai uzvārds sastāv tikai no burtiem.
            {
                Console.WriteLine("Invalid surname. Try again!");
            }
            else
            {
                userData[1] = Surname; // Ievieto uzvārdu masīvā.
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
            string fileName=pathToFile;
            bool emailExists = false;
            
            Console.WriteLine("\nEnter your email: ");
            Email = Console.ReadLine();
            
            // Atver failu lasīšanai.
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                string line;
                
                while ((line = sr.ReadLine()) != null) // Izlasa katru rindu.
                {
                    string[] parts = line.Split(','); // Sadala rindu pa vārdiem.
                    
                    if (parts[2] == Email) // Pārbauda vai e-pasts jau eksistē.
                    {
                        Console.WriteLine("This email is already in use.\nDo you want to cancel your registration?(Y/N): ");
                        string choice = Console.ReadLine();
                        
                        if (choice.ToLower() == "y")
                        {
                            Environment.Exit(0); // Iziet no programmas.
                        }
                        emailExists = true;
                        break;
                    }
                }
            }
            //Ja pasts neeksistē un tas ir uzrakstīts pareizi(), tad tas tiek pievienots masīvam.
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
            Console.WriteLine("\nEnter your phone number: ");
            PhoneNumber = Console.ReadLine();
            
            if (!Regex.IsMatch(PhoneNumber, @"^\+\d+$")) // Pārbauda numuru.
            {
                Console.WriteLine("The phone number must contain a plus at the beginning and be written without spaces. Try again!");
            }
            else
            {
                userData[3] = PhoneNumber; // Ievieto numuru masīvā.
                break;
            }
        } while (true);
    }

    //Pārbauda lietotāja paroli un neiziet no сikla, kamēr vards nav pareizs. 
    public void password()
    {
        do
        {
            Console.WriteLine("\nEnter your password: ");
            Password = Console.ReadLine();
            
            if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{8,}$")) // Pārbauda paroli.
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
            Console.WriteLine("\nEnter your password again: ");
            Password2 = Console.ReadLine();
            
            if (Password != Password2) // Pārbauda vai paroles sakrīt.
            {
                Console.WriteLine("Passwords do not match. Try again!");
            }
            else
            {
                userData[4] = Password2; // Ievieto paroli masīvā.
                // Izsauc metodi, kas ieraksta datus failā.
                file.Writer(userData[0], userData[1], userData[2], userData[3], userData[4]); 
                break;
            }
        } while (true);
    }
}
