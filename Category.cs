using System.Collections.Generic;

public class Category
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }

    // Konstruktors, kas piešķir vērtības mainīgajiem.
    public Category(string name)
    {
        Name = name;
        Products = new List<Product>();
    }
    
    // Metode, kas ļauj pievienot produktus kategorijai.
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }
}
