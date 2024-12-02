
using System.Xml.Linq;

namespace abstractfactory;

internal class AbstractFactory()
{
    public static void AbstractFactoryModel()
    {
        PizzaStore nyStore = new NYPizzaStore();
        PizzaStore chicagoStore = new ChicagoPizzaStore();

        Pizza pizza = nyStore.OrderPizza("cheese");
        Console.WriteLine($"Ethan ordered a {pizza.GetName()}\n");

        pizza = chicagoStore.OrderPizza("cheese");
        Console.WriteLine($"Joel ordered a {pizza.GetName()}\n");
    }
}

interface IDough
{
    string GetDoughType();
}
interface ISauce
{
    string GetSauceType();
}
interface ICheese
{
    string GetCheeseType();
}

class ThickCrustDough : IDough
{
    public string GetDoughType() => "Thick Crust Dough";
}
class ThinCrustDough : IDough
{
    public string GetDoughType() => "Thin Crust Dough";
}
class MarinaraSauce : ISauce
{
    public string GetSauceType() => "Marinara Sauce";
}
class PlumTomatoSauce : ISauce
{
    public string GetSauceType() => "Plum Tomato Sauce";
}
class ReggianoCheese : ICheese
{
    public string GetCheeseType() => "Reggiano Cheese";
}
class MozzarellaCheese : ICheese
{
    public string GetCheeseType() => "Mozzarella Cheese";
}

interface IPizzaIngredientFactory
{
    IDough CreateDough();
    ISauce CreateSauce();
    ICheese CreateCheese();
}

class NYPizzaIngredientFactory : IPizzaIngredientFactory
{
    public IDough CreateDough() => new ThinCrustDough();
    public ISauce CreateSauce() => new MarinaraSauce();
    public ICheese CreateCheese() => new ReggianoCheese();
}
class ChicagoPizzaIngredientFactory : IPizzaIngredientFactory
{
    public IDough CreateDough() => new ThickCrustDough();
    public ISauce CreateSauce() => new PlumTomatoSauce();
    public ICheese CreateCheese() => new MozzarellaCheese();
}

abstract class Pizza
{
    public string Name { get; set; }
    protected IDough Dough;
    protected ISauce Sauce;
    protected ICheese Cheese;
    public abstract void Prepare();
    public void Bake() => Console.WriteLine("Baking " + Name);
    public void Cut() => Console.WriteLine("Cutting " + Name);
    public void Box() => Console.WriteLine("Boxing " + Name);
    public string GetName() => Name;
}
// Concrete Products
class CheesePizza : Pizza
{
    private readonly IPizzaIngredientFactory _ingredientFactory;
    public CheesePizza(IPizzaIngredientFactory ingredientFactory)
    {
        _ingredientFactory = ingredientFactory;
    }
    public override void Prepare()
    {
        Console.WriteLine("Preparing " + Name);
        Dough = _ingredientFactory.CreateDough();
        Sauce = _ingredientFactory.CreateSauce();
        Cheese = _ingredientFactory.CreateCheese();
        Console.WriteLine("Tossing dough: " + Dough.GetDoughType());
        Console.WriteLine("Adding sauce: " + Sauce.GetSauceType());
        Console.WriteLine("Adding cheese: " + Cheese.GetCheeseType());
    }
}
// PizzaStore uses the Abstract Factory to create pizzas
abstract class PizzaStore
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
// NY and Chicago stores with their respective factories
class NYPizzaStore : PizzaStore
{
    protected override Pizza CreatePizza(string type)
    {
        Pizza pizza = null;
        IPizzaIngredientFactory ingredientFactory = new NYPizzaIngredientFactory();
        if (type.Equals("cheese", StringComparison.OrdinalIgnoreCase))
        {
            pizza = new CheesePizza(ingredientFactory) { Name = "New York Style Cheese Pizza" };
        }
        // Add more pizza types if needed
        return pizza;
    }
}
class ChicagoPizzaStore : PizzaStore
{
    protected override Pizza CreatePizza(string type)
    {
        Pizza pizza = null;
        IPizzaIngredientFactory ingredientFactory = new ChicagoPizzaIngredientFactory();
        if (type.Equals("cheese", StringComparison.OrdinalIgnoreCase))
        {
            pizza = new CheesePizza(ingredientFactory) { Name = "Chicago Style Cheese Pizza" };
        }
        // Add more pizza types if needed
        return pizza;
    }
}