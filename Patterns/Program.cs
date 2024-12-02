using decorator;
using factory;
using simplefactory;
using observer;
using abstractfactory;

namespace patterns;

class Program
{
    static void Main(string[] args)
    {
        var selection = UserSelection();

        switch (selection)
        {

            case "1":
                Strategy.StrategyModel();
                break;
            case "2":
                Observer.ObserverModel();
                break;
            case "3":
                Decorator.DecoratorModel();
                break;
            case "4":
                bool valid = false;
                string option = "";
                List<string> validInputs = new List<string>() { "1", "2", "3" };

                while (!valid)
                {
                    Console.WriteLine("Choose a sub option!");
                    Console.WriteLine("1) Simple Factory");
                    Console.WriteLine("2) Factory");
                    Console.WriteLine("3) Abstract Factory");
                    Console.WriteLine();
                    option = Console.ReadLine();

                    if (option != null && validInputs.Contains(option))
                    {
                        switch (option)
                        {
                            case "1":
                                SimpleFactory.SimpleFactoryModel();
                                break;

                            case "2":
                                Factory.FactoryModel(); 
                                break;
                            case "3":
                                AbstractFactory.AbstractFactoryModel();
                                break;
                        }
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Choose again");
                    }
            }
            break;
        }
    }

    public static string UserSelection()
    {
        bool valid = false;
        string option = "";
        List<string> validInputs = new List<string>() { "1", "2", "3", "4" };

        Console.WriteLine("Choose one option!");
        Console.WriteLine("1) Strategy Pattern");
        Console.WriteLine("2) Observer Pattern");
        Console.WriteLine("3) Decorator Pattern");
        Console.WriteLine("4) Factory Pattern");
        Console.WriteLine("");

        while (!valid)
        {
            option = Console.ReadLine();

            if (option != null && validInputs.Contains(option))
            {
                valid = true;
            }
            else
            {
                Console.WriteLine("Invalid option. Choose again");
            }
        }

        return option;
    }
}
