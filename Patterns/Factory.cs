
namespace factory;

internal class Factory()
{
    public static void FactoryModel()
    {
        PizzaStore nyStore = new NYPizzaStore();
        PizzaStore chicagoStore = new ChicagoPizzaStore();

        Pizza pizza = nyStore.OrderPizza("cheese");
        Console.WriteLine("Ethan ordered a " + pizza.name + "\n");

        pizza = chicagoStore.OrderPizza("cheese");
        Console.WriteLine("Joel ordered a " + pizza.name + "\n");
    }
}

public abstract class Pizza()
{
    public string name;
    public string dough;
    public string sauce;
    public List<string> toppings = new List<string>();
    public void Prepare()
    {
        Console.WriteLine("Preparing " + name);
        Console.WriteLine("Tossing dough...");
        Console.WriteLine("Adding sauce...");
        Console.WriteLine("Adding toppings: ");
        foreach (var topping in toppings)
        {
            Console.WriteLine("-" + topping);
        }
    }

    public void Bake()
    {
        Console.WriteLine($"Baking {name}");
    }

    public void Cut()
    {
        Console.WriteLine($"Cutting {name}");
    }

    public void Box()
    {
        Console.WriteLine($"Boxing {name}");
    }
}
public class NYStyleCheesePizza : Pizza
{
    public NYStyleCheesePizza()
    {
        name = "NY Style Sauce and Cheese Pizza";
        dough = "Thin Crust Dough";
        sauce = "Marinara Sauce";
        toppings.Add("Grated Reggiano Cheese");
    }
}

public class ChicagoStyleCheesePizza : Pizza
{
    public ChicagoStyleCheesePizza()
    {
        name = "Chicago Style Deep Dish Cheese Pizza";
        dough = "Extra Thick Crust Dough";
        sauce = "Plum Tomato Sauce";
        toppings.Add("Shredded Mozzarella Cheese");
    }
    public void Cut()
    {
        Console.WriteLine("Cutting the pizza into square slices");
    }
}


public abstract class PizzaStore
{
    public Pizza OrderPizza(string type)
    {
        Pizza pizza;

        pizza = CreatePizza(type);

        pizza.Prepare();
        pizza.Bake();
        pizza.Cut();
        pizza.Box();

        return pizza;
    }

    protected abstract Pizza CreatePizza(string type);
}

public class NYPizzaStore : PizzaStore
{
    protected override Pizza CreatePizza(string type)
    {
        if (type.Equals("cheese", StringComparison.OrdinalIgnoreCase))
        {
            return new NYStyleCheesePizza();
        }
        else
        {
            return null;
        }
    }
}

public class ChicagoPizzaStore : PizzaStore
{
    protected override Pizza CreatePizza(string type)
    {
        if (type.Equals("cheese", StringComparison.OrdinalIgnoreCase))
        {
            return new ChicagoStyleCheesePizza();
        }
        else
        {
            return null;
        }
    }
}
