namespace composite;

internal class CompositePattern
{
    public static void CompositeModel()
    {
        MenuComponent pancakeHouseMenu = 
            new Menu("PANCAKE HOUSE MENU", "Breakfast");
        MenuComponent dinerMenu = 
            new Menu("DINER MENU", "Lunch");
        MenuComponent cafeMenu = 
            new Menu("CAFE MENU", "Dinner");
        MenuComponent dessertMenu = 
            new Menu("DESSERT MENU", "Dessert of course!");
        MenuComponent allMenus = 
            new Menu("ALL MENUS", "All menus combined");

        allMenus.Add(pancakeHouseMenu);
        allMenus.Add(dinerMenu);
        allMenus.Add(cafeMenu);

        dinerMenu.Add(new MenuItem(
            "Pasta",
            "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
            true,
            3.89));
        dinerMenu.Add(dessertMenu);
        dessertMenu.Add(new MenuItem(
            "Apple Pie",
            "Apple pie with a flakey crust, topped with vanilla ice cream",
            true,
            1.59));

        Waitress waitress = new Waitress(allMenus);
        waitress.PrintMenu();
    }
}

public abstract class MenuComponent
{
    public virtual void Add(MenuComponent menuComponent)
    {
        throw new NotSupportedException();
    }
    public virtual void Remove(MenuComponent menuComponent)
    {
        throw new NotSupportedException();
    }
    public virtual MenuComponent GetChild(int i)
    {
        throw new NotSupportedException();
    }
    public virtual string GetName()
    {
        throw new NotSupportedException();
    }
    public virtual string GetDescription()
    {
        throw new NotSupportedException();
    }
    public virtual double GetPrice()
    {
        throw new NotSupportedException();
    }
    public virtual bool IsVegetarian()
    {
        throw new NotSupportedException();
    }
    public virtual void Print()
    {
        throw new NotSupportedException();
    }
}

public class MenuItem : MenuComponent
{
    private readonly string _name;
    private readonly string _description;
    private readonly bool _vegetarian;
    private readonly double _price;

    public MenuItem(string name,
        string description,
        bool vegetarian,
        double price)
    {
        _name = name;
        _description = description;
        _vegetarian = vegetarian;
        _price = price;
    }

    public override string GetName() => _name;
    public override string GetDescription() => _description;
    public override double GetPrice() => _price;
    public override bool IsVegetarian() => _vegetarian;

    public override void Print()
    {
        Console.Write($" {GetName()}");
        if (IsVegetarian())
        {
            Console.Write("(v)");
        }
        Console.WriteLine($", {GetPrice()}");
        Console.WriteLine($" -- {GetDescription()}");
    }
}

public class Menu : MenuComponent
{
    private readonly List<MenuComponent> _menuComponents = new();
    private readonly string _name;
    private readonly string _description;

    public Menu(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public override void Add(MenuComponent menuComponent)
    {
        _menuComponents.Add(menuComponent);
    }

    public override void Remove(MenuComponent menuComponent)
    {
        _menuComponents.Remove(menuComponent);
    }

    public override MenuComponent GetChild(int i)
    {
        return _menuComponents[i];
    }

    public override string GetName() => _name;
    public override string GetDescription() => _description;

    public override void Print()
    {
        Console.WriteLine($"\n{GetName()}");
        Console.WriteLine($", {GetDescription()}");
        Console.WriteLine("---------------------");

        foreach (MenuComponent menuComponent in _menuComponents)
        {
            menuComponent.Print();
        }
    }
}

public class Waitress
{
    private readonly MenuComponent _allMenus;

    public Waitress(MenuComponent allMenus)
    {
        _allMenus = allMenus;
    }

    public void PrintMenu()
    {
        _allMenus.Print();
    }
}