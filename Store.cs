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
        file = new File();
        shoppingCart = new ShoppingCart(card);


        // Izveido kategorijas un produktus.
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

        // Pievieno kategorijas sarakstam.
        categories.Add(clothing);
        categories.Add(electronics);
        categories.Add(homeAppliances);
        categories.Add(furniture);
        categories.Add(entertainment);
    }

    //Metode, kas parāda veikalu. Lietotājs var iegādāties produktus, pievienot tos grozam vai rediģēt konta iestatījumus.
    public void DisplayStore()
    {
        Card userCard = shoppingCart.GetUserCard();
        file.LogIn();
        file.writeShoppingCardInList(shoppingCart.products);
        
        while (true)
        {         
            Console.WriteLine("\nPlease choose an action:\n1. Enter credit card data or change credit card data and/or balance;\n2. Display products;\n3. Display cart;\n4. Account settings;\n5. Exit.");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":  // Savieno karti un tās atlikumu ar kontu.
                    file.WriteCardInAccounts();
                    break;
                
                case "2":  // Parāda produktus. Ir iespēja preci iegādāties uzreiz.
                    file.UpdateBalanceFromFile(card, userCard);
                    DisplayCategories(shoppingCart);
                    file.WriteShoppingCartInAccounts(shoppingCart.products);
                    file.ChangeBalanceAfterPurchase(userCard);
                    break;
                
                case "3":  // Parāda grozu. Ir iespēja iegādāties preci vai izņemt to no groza
                    shoppingCart.DisplayCart();
                    
                    Console.WriteLine("\nPlease choose an action:\n1.Return to main menu; \n2.Remove an item from the cart; \n3.Buy an item from the cart.");
                    string choise = Console.ReadLine();
                    
                    switch (choise)
                    {
                        case "1": // Atgriežas uz galveno izvēlni.
                            break;
                        
                        case "2":  // Izņem no groza.
                            shoppingCart.deleteFromCart();
                            file.WriteShoppingCartInAccounts(shoppingCart.products);
                            break;
                        
                        case "3": // Pirk produktu.
                            file.UpdateBalanceFromFile(card, userCard);
                            shoppingCart.BuyProductInCart();
                            file.WriteShoppingCartInAccounts(shoppingCart.products);
                            file.ChangeBalanceAfterPurchase(userCard);
                            break;
                        
                        default:
                            Console.WriteLine("Inccorect input!");
                            break;
                    }
                    break;
                
                case "4": // Izvēlne par konta iestatījumiem.
                    Console.WriteLine("\nPlease choose an action:\n1.Return to main menu; \n2.Change password; \n3.Delete account.");
                    string choise2 = Console.ReadLine();
                    
                    switch (choise2)
                    {
                        case "1": // Atgriežas uz galveno izvēlni.
                            break;
                        
                        case "2": // Mainīga paroli.
                            file.changePassword();
                            break;
                        
                        case "3": // Dzēš kontu.
                            bool accountDeleted = file.deleteAccount();
                            if (accountDeleted)
                            {
                                return;
                            }
                            else
                            {
                                break;
                            }
                        
                        default:
                            Console.WriteLine("Inccorect input!");
                            break;
                    }
                    break;
                
                case "5": // Iziet no veikala.
                    Console.WriteLine("Thank you for using our store. Goodbye!");
                    return;
                
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    //Metode, kas ļauj izvēlēties kategoriju un pēc tam izvēlēties produktu.
    public void DisplayCategories(ShoppingCart shoppingCart)
    {
        int categoryNumber;

        do 
        {
            Console.WriteLine("\nAvailable categories:");

            for (int i = 0; i < categories.Count; i++) // Parāda kategorijas.
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }

            Console.WriteLine("Enter the category number to view products (or '0' to go back):");
            string input = Console.ReadLine();

            // Pārbauda vai ievadītais skaitlis ir derīgs.
            if (int.TryParse(input, out categoryNumber) && categoryNumber >= 1 && categoryNumber <= categories.Count) 
            {
                DisplayProductsInCategory(categories[categoryNumber - 1], shoppingCart); 
            }
            else if (input == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid category number.");
            }
        } while (categoryNumber != 0); // Atkārto ciklu, kamēr nav ievadīts 0.
    }

   //Metode, kas ļauj izvēlēties produktu un pēc tam izvēlēties, vai to pirkt vai pievienot grozam.
    private void DisplayProductsInCategory(Category category, ShoppingCart shoppingCart)
    {
        int productNumber;
    
        do
        {
            Console.WriteLine($"\nProducts in {category.Name}:");
            int counter = 1;
            foreach (var product in category.Products) // Parāda produktus.
            {
                Console.WriteLine($"{counter}. {product.Name} - {product.Price} EUR");
                counter++;
            }

            Console.WriteLine("Enter the product number to add to cart or buy it (or '0' to go back):");
            string input = Console.ReadLine();

            // Pārbauda vai ievadītais skaitlis ir derīgs.
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
                    shoppingCart.AddProduct(category.Products[productNumber - 1]); // Pievieno produktu grozam.
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
