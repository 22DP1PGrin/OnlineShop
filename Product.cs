public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }

    // Konstruktors, kas piešķir vērtības mainīgajiem.
    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }
}
