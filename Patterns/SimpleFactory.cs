
namespace simplefactory;

internal class SimpleFactory()
{
    public static void SimpleFactoryModel()
    {
        SimplePizzaFactory factory = new SimplePizzaFactory();
        PizzaStore store = new PizzaStore(factory);

        Pizza pizza = store.OrderPizza("cheese");
        Console.WriteLine($"Ordered a {pizza.Name}\n");

        pizza = store.OrderPizza("pepperoni");
        Console.WriteLine($"Ordered a {pizza.Name}\n");
    }
}

public abstract class Pizza()
{
    public string Name { get; protected set; }

    public void Prepare()
    {
        Console.WriteLine($"Preparing {Name}");
    }

    public void Bake()
    {
        Console.WriteLine($"Baking {Name}");
    }

    public void Cut()
    {
        Console.WriteLine($"Cutting {Name}");
    }

    public void Box()
    {
        Console.WriteLine($"Boxing {Name}");
    }
}
class CheesePizza : Pizza
{
    public CheesePizza()
    {
        Name = "Cheese Pizza";
    }
}

class PepperoniPizza : Pizza
{
    public PepperoniPizza()
    {
        Name = "Pepperoni Pizza";
    }
}

class VeggiePizza : Pizza
{
    public VeggiePizza()
    {
        Name = "Veggie Pizza";
    }
}

public class PizzaStore
{
    private readonly SimplePizzaFactory _factory;

    public PizzaStore(SimplePizzaFactory factory)
    {
        _factory = factory;
    }

    public Pizza OrderPizza(string type)
    {
        Pizza pizza;

        pizza = _factory.CreatePizza(type);

        pizza.Prepare();
        pizza.Bake();
        pizza.Cut();
        pizza.Box();

        return pizza;
    }
}


public class SimplePizzaFactory
{
    public Pizza CreatePizza(string type)
    {
        Pizza pizza = null;

        if (type.Equals("cheese", StringComparison.OrdinalIgnoreCase))
        {
            pizza = new CheesePizza();
        }
        else if (type.Equals("pepperoni", StringComparison.OrdinalIgnoreCase))
        {
            pizza = new PepperoniPizza();
        }
        else if (type.Equals("veggie", StringComparison.OrdinalIgnoreCase))
        {
            pizza = new VeggiePizza();
        }

        return pizza;
    }
}

