using System.IO;

public class File
{
    public void Writer(string Name, string Surname, string PhoneNumber, string Email, string Password)
    {
        string path = @"C:\Users\Admin\source\repos\ConsoleApp1\ConsoleApp1\Accounts.csv";
        try
        {
            using(StreamWriter sw = new StreamWriter(path,true, System.Text.Encoding.Default))
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
}
