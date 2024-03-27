using System;
using System.Text;
using System.IO;
using System.Xml.Linq;
public class File
{
    string email = "";
    
    string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";
    //string path = @"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts.csv";
    
    //Metode, kas ieraksta lietotāja datus csv failā.
    public void Writer(string User)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.Write(User+",");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void Accounts()
    {
        //string path = @"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts.csv";
        string path = @"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts.csv";

        //string path = @"C:\Users\Admin\source\repos\Eksamens\Eksamens\Accounts.csv";
        using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string email2 = parts[3];
                string fileName = $"{email2}.csv";
                string fullFilePath = Path.Combine(@"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts\", fileName);
                FileInfo fileInf = new FileInfo(fullFilePath);

                if (!fileInf.Exists)
                {
                    using (FileStream fs = fileInf.Create())
                    {
                    }
                }
            }
        }
    }
    
    public void WriteCardInAccounts(string Number, string ExpirationDate, string CVV)
    {

        Console.WriteLine(email);
        string fileName = $"{email}.csv";
        string fullPath = Path.Combine(@"C:\Users\pgrin\source\repos\Eksamens\ConsoleApp3\Accounts\", fileName);

        try
        {
            using (StreamWriter sw = new StreamWriter(fullPath, true, System.Text.Encoding.Default))
            {
                string line = $"{Number},{ExpirationDate},{CVV}";
                sw.WriteLine(line);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
