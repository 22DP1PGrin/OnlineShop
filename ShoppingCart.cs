using System;
using System.Collections.Generic;

  public class ShoppingCart
  {
      public List<Product> products;
      private Card userCard;

      public ShoppingCart(Card card)
      {
          userCard = card;
          products = new List<Product>();
      }
      public void AddProduct(Product product)
      {
          products.Add(product);
          Console.WriteLine($"{product.Name} added to your shopping cart.");
      }


    public void RemoveProduct(Product product)
    {
        if (products.Remove(product))
        {
            Console.WriteLine($"{product.Name} removed from your shopping cart.");
        }
        else
        {
            Console.WriteLine($"{product.Name} is not in your shopping cart.");
        }
    }

    public void DisplayCart()
    {
        Console.WriteLine("Your shopping cart:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} - €{products[i].Price}");
        }
        double totalPrice = TotalPrice();
        Console.WriteLine($"Total: €{Math.Round(totalPrice, 2)}");
        Console.WriteLine("Enter the product number to buy (or '0' to go back):");
        string input = Console.ReadLine();
        int productNumber;
        if (int.TryParse(input, out productNumber) && productNumber >= 1 && productNumber <= products.Count)
        {
            BuyProduct(products[productNumber - 1]);
        }
        else if (input == "0")
        {
            return;
        }
        else
        {
            Console.WriteLine("Invalid product number.");
        }
    }

    public void BuyProduct(Product product)
    {
        double totalPrice = product.Price;
        if (totalPrice > userCard.Balance)
        {
            Console.WriteLine("You don't have enough balance to buy this product.");
            return;
        }

        userCard.Balance -= totalPrice;
        Console.WriteLine($"Product '{product.Name}' bought successfully! Your new balance is €{userCard.Balance}");
        products.Remove(product);
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

    public void BuyProducts()
    {
        double totalPrice = TotalPrice();
        if (totalPrice > userCard.Balance)
        {
            Console.WriteLine("You don't have enough balance to buy this product.");
            return;
        }

        userCard.Balance -= totalPrice;
        Console.WriteLine($"Product bought successfully! Your new balance is €{userCard.Balance}");
    }
    public void deleteFromCart()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Your shopping cart is empty. Nothing to delete.");
            return;
        }
        
        int productNumber;
        
        do
        {
            Console.WriteLine("Enter the product number to delete from your shopping cart(or '0' to go main menu): ");
            string product = Console.ReadLine();
            
            if (int.TryParse(product, out productNumber) && productNumber >= 1 && productNumber <= products.Count)
            {
                products.RemoveAt(productNumber - 1);
                Console.WriteLine($"Product {productNumber} deleted from your shopping cart.");
                DisplayCart();
            }
            else if (productNumber == 0)
            {
                Console.WriteLine("Going back...");
                return;
            }
            else
            {
                Console.WriteLine("Inccorect input!");
            }
        } while (productNumber!= 0);
    }
}
