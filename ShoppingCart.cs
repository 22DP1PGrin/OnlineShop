using System;
using System.Collections.Generic;

public class ShoppingCart
{
    private List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"{product.name} added to your shopping cart.");
    }

    public void RemoveProduct(Product product)
    {
        if (products.Remove(product))
        {
            Console.WriteLine($"{product.name} removed from your shopping cart.");
        }
        else
        {
            Console.WriteLine($"{product.name} is not in your shopping cart.");
        }
    }

    public void DisplayCart()
    {
       Console.WriteLine("Your shopping cart:");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.name} - ${product.Price}");
        }
        Console.WriteLine($"Total: ${TotalPrice()}");
    }

    private double TotalPrice()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price;
        }
        return total;
    }
}
