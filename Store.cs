using System;
using System.Collections.Generic;


public class Store
{
    private List<Category> categories = new List<Category>();

    public Store()
    {
        Category clothing = new Category("Clothing");
        clothing.AddProduct(new Product("T-Shirt", 15.35));
        clothing.AddProduct(new Product("Jeans", 11.97));
        clothing.AddProduct(new Product("Dress", 25.63));
        clothing.AddProduct(new Product("Sweater", 18.84));
        clothing.AddProduct(new Product("Jacket", 47.26));

        Category electronics = new Category("Electronics");
        electronics.AddProduct(new Product("Phone", 448.59));
        electronics.AddProduct(new Product("Laptop", 599.99));
        electronics.AddProduct(new Product("Headphones", 89.99));
        electronics.AddProduct(new Product("Tablet", 299.99));
        electronics.AddProduct(new Product("Keyboard", 24.99));
        electronics.AddProduct(new Product("Speakers", 29.99));

        Category homeAppliances = new Category("Home Appliances");
        homeAppliances.AddProduct(new Product("Refrigerator", 328.99));
        homeAppliances.AddProduct(new Product("Washing Machine", 559.99));
        homeAppliances.AddProduct(new Product("Microwave", 149.99));
        homeAppliances.AddProduct(new Product("Blender", 29.99));

        Category furniture = new Category("Furniture");
        furniture.AddProduct(new Product("Office chair", 49.99));
        furniture.AddProduct(new Product("Gaming chair", 299.99));
        furniture.AddProduct(new Product("Computer table", 129.99));
        furniture.AddProduct(new Product("Sofa", 349.99));
        furniture.AddProduct(new Product("Bed", 219.99));
        furniture.AddProduct(new Product("Closet", 324.99));
        furniture.AddProduct(new Product("Shelf", 89.99));

        Category entertainment = new Category("Hobby and entertainment");
        entertainment.AddProduct(new Product("Football ball", 14.99));
        entertainment.AddProduct(new Product("Chess", 9.99));
        entertainment.AddProduct(new Product("Fishing rod", 29.99));
        entertainment.AddProduct(new Product("Color pencils", 6.99));
        entertainment.AddProduct(new Product("Anime figure", 19.99));
        entertainment.AddProduct(new Product("Bycycle", 199.99));
        entertainment.AddProduct(new Product("C# programming book", 11.99));


        categories.Add(clothing);
        categories.Add(electronics);
        categories.Add(homeAppliances);
        categories.Add(furniture);
        categories.Add(entertainment);
    }

    public void DisplayStore()
    {
        Card card = new Card();
        ShoppingCart shoppingCart = new ShoppingCart();
        while (true)
        {
            Console.WriteLine("\nPlease choose an action:");
            Console.WriteLine("1. Enter credit card data");
            Console.WriteLine("2. Display categories");
            Console.WriteLine("3. Display cart");
            Console.WriteLine("4. Delete account");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    card.EnterCreditCardData();
                    card.SetBalance();
                    break;
                case "2":
                    DisplayCategories();
                    break;
                case "3":
                    shoppingCart.DisplayCart();
                    break;
                case "4":

                case "5":
                    Console.WriteLine("Thank you for using our store. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
    public void DisplayCategories()
    {
        Console.WriteLine("Available categories:");
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i].Name}");
        }

        Console.WriteLine("Enter the category number to view products (or '0' to go back):");
        string input = Console.ReadLine();
        int categoryNumber;
        if (int.TryParse(input, out categoryNumber) && categoryNumber >= 1 && categoryNumber <= categories.Count)
        {
            DisplayProductsInCategory(categories[categoryNumber - 1]);
        }
        else if (input == "0")
        {
            return;
        }
        else
        {
            Console.WriteLine("Invalid category number.");
        }
    }

    private void DisplayProductsInCategory(Category category)
    {
        ShoppingCart shoppingCart = new ShoppingCart();
        Console.WriteLine($"Products in {category.Name}:");
        foreach (var product in category.Products)
        {
            Console.WriteLine($"{product.Name} - €{product.Price}");
        }

        Console.WriteLine("Enter the product number to add to cart (or '0' to go back):");
        string input = Console.ReadLine();
        int productNumber;
        if (int.TryParse(input, out productNumber) && productNumber >= 1 && productNumber <= category.Products.Count)
        {
            shoppingCart.AddProduct(category.Products[productNumber - 1]);
            Console.WriteLine("Product added to cart!");
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
}
