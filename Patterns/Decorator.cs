using System.Transactions;

namespace patterns;

internal class Decorator()
{
    public static void DecoratorModel()
    {
        Decorator weatherData = new Decorator();

    }
}

public abstract class Beverage
{
    string description = "Unknown Beverage";

    public string getDescription()
    {
        return description;
    }


    public abstract double cost();
}
