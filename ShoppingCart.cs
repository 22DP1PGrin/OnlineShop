using System;
using System.Collections.Generic;

public class ShoppingCart
{
    public List<Product> products;
    private Card userCard;

    // Konstruktors, kas piešķir vērtības mainīgajiem.
    public ShoppingCart(Card card)
    {
        userCard = card;
        products = new List<Product>();
    }

    // Metode, kas nodrošina piekļuvi mainīgajam ārpus klases.
    public Card GetUserCard()
    {
        return userCard;
    }
    
    // Metode, kas ļauj pievienot produktu grozam.
    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"{product.Name} added to your shopping cart.");
    }

    // Metode, kas ļauj apskatīt grozu.
    public void DisplayCart()
    {
        Console.WriteLine("\nYour shopping cart: ");
        
        // Izvada produktu nosaukumus un cenas.
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} EUR.");
        }
        double totalPrice = TotalPrice();
        // Izvada kopējo cenu, noapaļojot līdz diviem cipariem aiz komata.
        Console.WriteLine($"\nTotal: {Math.Round(totalPrice, 2)} EUR."); 
    }

    // Atrod visu grozā esošo preču izmaksas.
    private double TotalPrice()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price;
        }
        return total;
    }

    //Metode, kas ļauj iegādāties preces no groza.
    public void BuyProductInCart()
    {
        if (products.Count == 0) // Pārbauda vai grozs ir tukšs.
        {
            Console.WriteLine("Your shopping cart is empty. Nothing to buy.");
            return;
        }
        int productNumber;
        
        do
        {
            Console.WriteLine("\nEnter the product number to buy from your shopping cart(or '0' to go main menu): ");
            string Product = Console.ReadLine();

            // Pārbauda vai ievadītais skaitlis ir derīgs.
            if (int.TryParse(Product, out productNumber) && productNumber >= 1 && productNumber <= products.Count)
            {
                Product productToBuy = products[productNumber - 1]; // Atrod izvēlēto produktu.
                double totalPrice = productToBuy.Price; // Iegūst izvēlēto produktu cenu.
                if (totalPrice > userCard.Balance) // Pārbauda vai kartes atlikums pietiek.
                {
                    Console.WriteLine("You don't have enough balance to buy this product.");
                    return;
                }
                userCard.Balance=Math.Round((userCard.Balance-totalPrice),2); // Atņem no kartes atlikumu.
                Console.WriteLine($"Product bought successfully! Your new balance is {userCard.Balance} EUR.");

                products.RemoveAt(productNumber - 1); // Izņem izvēlēto produktu no saraksta.
            }
            else if (productNumber == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid product number.");
            }
        } while (productNumber != 0);
    }

    //Metode, kas ļauj iegādāties preci, nepievienojot to grozam.
    public void BuyProduct(Product product)
    {
        double totalPrice = product.Price; // Atrod preces cenu.
        if (totalPrice > userCard.Balance) // Pārbauda vai kartes atlikums pietiek.
        {
            Console.WriteLine("You don't have enough balance to buy this product.");
            return;
        }
        
        // No kartes noņem preces izmaksas. Noapaļo atlikums līdz diviem cipariem aiz komata.
        userCard.Balance=Math.Round((userCard.Balance-totalPrice),2);
        Console.WriteLine($"Product bought successfully! Your new balance is {userCard.Balance} EUR.");
    }
    
    // Metode, kas ļauj izdzēst produktu no groza.
    public void deleteFromCart()
    {
        if (products.Count == 0) // Pārbauda vai grozs ir tukšs.
        {
            Console.WriteLine("Your shopping cart is empty. Nothing to delete.");
            return;
        }
        int productNumber;

        do
        {
            Console.WriteLine("\nEnter the product number to delete from your shopping cart(or '0' to go main menu): ");
            string product = Console.ReadLine();

            // Pārbauda vai ievadītais skaitlis ir derīgs.
            if (int.TryParse(product, out productNumber) && productNumber >= 1 && productNumber <= products.Count)
            {
                products.RemoveAt(productNumber - 1);// Izņem izvēlēto produktu no saraksta.
                Console.WriteLine($"Product deleted from your shopping cart.");
                
                DisplayCart();
            }
            else if (productNumber == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid product number.");
            }
        } while (productNumber != 0);
    }
}
