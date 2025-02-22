using decorator;
using factory;
using simplefactory;
using observer;
using abstractfactory;
using singleton;
using command;
using adapter;
using templatemethod;
using iterator;
using composite;

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

            case "5":
                SingletonPattern.SingletonModel();
                break;

            case "6":
                CommandPattern.CommandModel();
                break;

            case "7":
                AdapterPattern.AdapterModel();
                break;

            case "8":
                TemplateMethod.TemplateMethodModel();
                break;

            case "9":
                IteratorPattern.IteratorModel();
                break;

            case "10":
                CompositePattern.CompositeModel();
                break;
        }
    }

    public static string UserSelection()
    {
        bool valid = false;
        string option = "";
        List<string> validInputs = Enumerable.Range(1, 10).Select(n => n.ToString()).ToList();

        Console.WriteLine("Choose one option!");
        Console.WriteLine("1) Strategy Pattern");
        Console.WriteLine("2) Observer Pattern");
        Console.WriteLine("3) Decorator Pattern");
        Console.WriteLine("4) Factory Pattern");
        Console.WriteLine("5) Singleton Pattern");
        Console.WriteLine("6) Command Pattern");
        Console.WriteLine("7) Adapter Pattern");
        Console.WriteLine("8) Template Method Pattern");
        Console.WriteLine("9) Iterator Pattern");
        Console.WriteLine("10) Composite Pattern");
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
