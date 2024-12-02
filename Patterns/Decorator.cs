
namespace decorator;

internal class Decorator()
{
    public static void DecoratorModel()
    {
        Beverage beverage = new Expresso();
        Console.WriteLine($"{beverage.getDescription()} ${beverage.cost()}");

        Beverage beverage2 = new DarkRoast();
        beverage2 = new Mocha(beverage2);
        beverage2 = new Mocha(beverage2);
        beverage2 = new Whip(beverage2);
        Console.WriteLine($"{beverage2.getDescription()} ${beverage2.cost()}");

        Beverage beverage3 = new HouseBlend();
        beverage3 = new Soy(beverage3);
        beverage3 = new Mocha(beverage3);
        beverage3 = new Whip(beverage3);
        Console.WriteLine($"{beverage3.getDescription()} ${beverage3.cost()}");
    }
}

public abstract class Beverage
{
    public string description = "Unknown Beverage";

    public virtual string getDescription()
    {
        return description;
    }


    public abstract double cost();
}

public abstract class CondimentDecorator : Beverage
{
    public Beverage beverage;
    public override abstract string getDescription();
}

public class HouseBlend : Beverage
{
    public HouseBlend()
    {
        description = "House Blend Coffee";
    }

    public override double cost()
    {
        return 0.89;
    }
}

public class Expresso : Beverage
{
    public Expresso()
    {
        description = "Expresso";
    }

    public override double cost()
    {
        return 1.99;
    }
}

public class DarkRoast : Beverage
{
    public DarkRoast()
    {
        description = "Dark Roast";
    }

    public override double cost()
    {
        return .99;
    }
}

public class Decaf : Beverage
{
    public Decaf()
    {
        description = "Decaf";
    }

    public override double cost()
    {
        return 1.05;
    }
}

public class Mocha : CondimentDecorator
{
    public Mocha(Beverage beverage)
    {
        this.beverage = beverage;
    }

    public override string getDescription()
    {
        return beverage.getDescription() + " Mocha";
    }

    public override double cost()
    {
        return beverage.cost() + .2;
    }
}

public class Soy : CondimentDecorator
{
    public Soy(Beverage beverage)
    {
        this.beverage = beverage;
    }

    public override string getDescription()
    {
        return beverage.getDescription() + " Soy";
    }

    public override double cost()
    {
        return beverage.cost() + .15;
    }
}

public class Whip : CondimentDecorator
{
    public Whip(Beverage beverage)
    {
        this.beverage = beverage;
    }

    public override string getDescription()
    {
        return beverage.getDescription() + " Whip";
    }

    public override double cost()
    {
        return beverage.cost() + .1;
    }
}