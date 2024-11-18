namespace patterns;

class Program
{
    static void Main(string[] args)
    {
        var selection = UserSelection();

        switch(selection)
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
        }
    }

    public static string UserSelection()
    {
        bool valid = false;
        string option = "";
        List<string> validInputs = new List<string>() { "1", "2", "3" };

        Console.WriteLine("Choose one option!");
        Console.WriteLine("1) Strategy Model");
        Console.WriteLine("2) Observer Model");
        Console.WriteLine("3) Decorator Model");
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
