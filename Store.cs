using System;
using System.Collections.Generic;

public class Store
{
    private List<Category> categories = new List<Category>();
    private Card card;
    private ShoppingCart shoppingCart;
    private File file;

    public Store()
    {
        card = new Card();
        shoppingCart = new ShoppingCart(card);

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
        File file = new File();
        ShoppingCart shoppingCart = new ShoppingCart(card);
        file.LogIn();
        file.writeShoppingCardInList(shoppingCart.products);
        while (true)
        {
            Console.WriteLine("\nPlease choose an action:\n1. Enter credit card data or change credit card data and/or balance\n2. Display products\n3. Display cart\n4. Account settings\n5. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    
                    file.WriteCardInAccounts(card.CreditCardNumber, card.CardExpirationDate, card.CardCVV, card.Balance);
                    card.EnterCreditCardData();
                    card.SetBalance();
                    file.WriteCardInAccounts(card.CreditCardNumber, card.CardExpirationDate, card.CardCVV, card.Balance);
                    break;
                case "2":
                    DisplayCategories(shoppingCart);
                    file.WriteShoppingCartInAccounts(shoppingCart.products);
                    break;
                case "3":
                    shoppingCart.DisplayCart();
                    Console.WriteLine("\nPlease choose an action:\n1.Return to main menu \n2.Remove an item from the cart \n3.Buy an item from the cart");
                    string choise = Console.ReadLine();
                    switch(choise)
                    {
                        case "1":
                            break;
                        case "2":
                            shoppingCart.deleteFromCart();
                            file.WriteShoppingCartInAccounts(shoppingCart.products);
                            break;
                        case "3":
                            break;
                        default:
                            Console.WriteLine("Inccorect input!");
                            break;
                    }
                    continue;
                case "4":
                    Console.WriteLine("\nPlease choose an action:\n1.Return to main menu \n2. Change password \n3.Delete account");
                    string choise2 = Console.ReadLine();
                    switch(choise2)
                    {
                        case "1":
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        default:
                            Console.WriteLine("Inccorect input!");
                            break;
                    }
                    break;
                case "5":
                    Console.WriteLine("Thank you for using our store. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    public void DisplayCategories(ShoppingCart shoppingCart)
    {
       
        int categoryNumber;
        
        do {
            Console.WriteLine("Available categories:");
            
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            
            Console.WriteLine("Enter the category number to view products (or '0' to go back):");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out categoryNumber) && categoryNumber >= 1 && categoryNumber <= categories.Count)
            {
                DisplayProductsInCategory(categories[categoryNumber - 1], shoppingCart); // Pass shoppingCart as parameter

            }
            else if (input == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid category number.");
            }
        } while (categoryNumber!=0);
    }

    private void DisplayProductsInCategory(Category category, ShoppingCart shoppingCart)
    {
        int productNumber;
        do
        {
            Console.WriteLine($"Products in {category.Name}:");
            int counter = 1;
            foreach (var product in category.Products)
            {
                Console.WriteLine($"{counter}. {product.Name} - €{product.Price}");
                counter++;
            }

            Console.WriteLine("Enter the product number to add to cart or buy it (or '0' to go back):");
            string input = Console.ReadLine();

            if (int.TryParse(input, out productNumber) && productNumber >= 1 && productNumber <= category.Products.Count)
            {
                Console.WriteLine("Do you want to buy the product? (Y/N)");
                string buyChoice = Console.ReadLine().ToUpper();

                if (buyChoice == "Y")
                {
                    shoppingCart.BuyProduct(category.Products[productNumber - 1]);
                }
                else if (buyChoice == "N")
                {
                    shoppingCart.AddProduct(category.Products[productNumber - 1]);
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            else if (input == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid product number.");
            }
        } while (productNumber != 0);
    }  
}
