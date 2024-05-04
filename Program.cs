class Program
{
   // Main metode, kas izsauc visas nepieciešamās metodes un klases.
   static void Main(string[] args)
    {
      Registration registartion = new Registration();
      Store store = new Store();
        
      registartion.Register();
      store.DisplayStore();
    }
}
