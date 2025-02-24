namespace factory;

internal class Factory()
{
    public static void FactoryModel()
    {
        PizzaStore nyStore = new NYPizzaStore();
        PizzaStore chicagoStore = new ChicagoPizzaStore();

        Pizza pizza = nyStore.OrderPizza("cheese");
        Console.WriteLine($"Ethan ordered a {pizza.Name}\n");

        pizza = chicagoStore.OrderPizza("cheese");
        Console.WriteLine($"Joel ordered a {pizza.Name}\n");

        pizza = nyStore.OrderPizza("veggie");
        Console.WriteLine($"Ethan ordered a {pizza.Name}\n");

        pizza = chicagoStore.OrderPizza("veggie");
        Console.WriteLine($"Joel ordered a {pizza.Name}\n");
    }
}

public abstract class Pizza
{
    public string Name { get; protected set; }
    public string Dough { get; protected set; }
    public string Sauce { get; protected set; }
    public List<string> Toppings { get; } = new();

    protected Pizza(string name, string dough, string sauce)
    {
        this.Name = name;
        this.Dough = dough;
        this.Sauce = sauce;
    }

    public void Prepare()
    {
        Console.WriteLine($"Preparing {Name}");
        Console.WriteLine("Tossing dough...");
        Console.WriteLine("Adding sauce...");
        Console.WriteLine("Adding toppings: ");
        Toppings.ForEach(topping => Console.WriteLine($"   {topping}"));
    }

    public virtual void Bake()
    {
        Console.WriteLine("Bake for 25 minutes at 350");
    }

    public virtual void Cut()
    {
        Console.WriteLine("Cutting the pizza into diagonal slices");
    }

    public virtual void Box()
    {
        Console.WriteLine("Place pizza in official PizzaStore box");
    }
}

public class NYStyleCheesePizza : Pizza
{
    public NYStyleCheesePizza() 
        : base("NY Style Sauce and Cheese Pizza", "Thin Crust Dough", "Marinara Sauce")
    {
        Toppings.Add("Grated Reggiano Cheese");
    }
}

public class NYStyleVeggiePizza : Pizza
{
    public NYStyleVeggiePizza()
        : base("NY Style Veggie Pizza", "Thin Crust Dough", "Marinara Sauce")
    {
        Toppings.Add("Grated Reggiano Cheese");
        Toppings.Add("Garlic");
        Toppings.Add("Onion");
        Toppings.Add("Mushrooms");
        Toppings.Add("Red Pepper");
    }
}

public class ChicagoStyleCheesePizza : Pizza
{
    public ChicagoStyleCheesePizza()
        : base("Chicago Style Deep Dish Cheese Pizza", "Extra Thick Crust Dough", "Plum Tomato Sauce")
    {
        Toppings.Add("Shredded Mozzarella Cheese");
    }

    public override void Cut()
    {
        Console.WriteLine("Cutting the pizza into square slices");
    }
}

public class ChicagoStyleVeggiePizza : Pizza
{
    public ChicagoStyleVeggiePizza()
        : base("Chicago Deep Dish Veggie Pizza", "Extra Thick Crust Dough", "Plum Tomato Sauce")
    {
        Toppings.Add("Shredded Mozzarella Cheese");
        Toppings.Add("Black Olives");
        Toppings.Add("Spinach");
        Toppings.Add("Eggplant");
    }

    public override void Cut()
    {
        Console.WriteLine("Cutting the pizza into square slices");
    }
}

public abstract class PizzaStore
{
    public Pizza OrderPizza(string type)
    {
        Pizza pizza = CreatePizza(type);

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
        return type switch
        {
            "cheese" => new NYStyleCheesePizza(),
            "veggie" => new NYStyleVeggiePizza(),
            _ => throw new ArgumentException("Invalid pizza type", nameof(type))
        };
    }
}

public class ChicagoPizzaStore : PizzaStore
{
    protected override Pizza CreatePizza(string type)
    {
        return type switch
        {
            "cheese" => new ChicagoStyleCheesePizza(),
            "veggie" => new ChicagoStyleVeggiePizza(),
            _ => throw new ArgumentException("Invalid pizza type", nameof(type))
        };
    }
}
