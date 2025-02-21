namespace iterator;

internal class IteratorPattern
{
    public static void IteratorModel()
    {
        var pancakeHouseMenu = new PancakeHouseMenu();
        var dinerMenu = new DinerMenu();
        var cafeMenu = new CafeMenu();

        var waitress = new Waitress(pancakeHouseMenu, dinerMenu, cafeMenu);

        Console.WriteLine("ALL MENUS");
        waitress.PrintMenu();

        Console.WriteLine("\nVEGETARIAN MENUS");
        waitress.PrintVegetarianMenu();
    }
}

public class MenuItem
{
    public string Name { get; }
    public string Description { get; }
    public bool IsVegetarian { get; }
    public double Price { get; }

    public MenuItem(string name, string description, bool vegetarian, double price)
    {
        this.Name = name;
        this.Description = description;
        this.IsVegetarian = vegetarian;
        this.Price = price;
    }
}

public interface IMenu
{
    IEnumerable<MenuItem> GetMenuItems();
}


public class CafeMenu : IMenu
{
    private readonly Dictionary<string, MenuItem> menuItems = [];

    public CafeMenu() 
    {
        AddItem("Veggie Burger and Air Fries",
            "Veggie burger on a whole wheat bun, lettuce, tomato, and fries",
            true, 3.99);
        AddItem("Soup of the day",
            "A cup of the soup of the day, with a side salad",
            false, 3.69);
        AddItem("Burrito",
            "A large burrito, with whole pinto beans, salsa, guacamole",
            true, 4.29);
    }

    public void AddItem(string name, string description, bool isVegetarian, double price)
    {
        MenuItem menuItem = new MenuItem(name, description, isVegetarian, price);
        menuItems.Add(name, menuItem);
    }

    public IEnumerable<MenuItem> GetMenuItems() => menuItems.Values;
}

public class DinerMenu : IMenu
{
    private readonly MenuItem[] _menuItems;
    private int _numberOfItems = 0;
    private const int MAX_ITEMS = 6;

    public DinerMenu()
    {
        _menuItems = new MenuItem[MAX_ITEMS];

        AddItem("Vegetarian BLT",
            "(Fakin') Bacon with lettuce & tomato on whole wheat",
            true, 2.99);
        AddItem("BLT",
            "Bacon with lettuce & tomato on whole wheat",
            false, 2.99);
        AddItem("Soup of the day",
            "Soup of the day, with a side of potato salad",
            false, 3.29);
    }

    public void AddItem(string name, string description, bool isVegetarian, double price)
    {
        if (_numberOfItems > MAX_ITEMS)
        {
            Console.WriteLine("Sorry, menu is full! Can't add item to menu");
            return;
        }

        MenuItem menuItem = new MenuItem(name, description, isVegetarian, price);
        _menuItems[_numberOfItems] = menuItem;
        _numberOfItems++;
    }

    public IEnumerable<MenuItem> GetMenuItems() => _menuItems.Take(_numberOfItems);
}

public class PancakeHouseMenu : IMenu
{
    private readonly List<MenuItem> _menuItems = new();

    public PancakeHouseMenu()
    {
        AddItem("K&B's Pancake Breakfast",
            "Pancakes with scrambled eggs and toast",
            true, 2.99);
        AddItem("Regular Pancake Breakfast",
            "Pancakes with fried eggs, sausage",
            false, 2.99);
        AddItem("Blueberry Pancakes",
            "Pancakes made with fresh blueberries",
            true, 3.49);
    }

    public void AddItem(string name, string description, bool vegetarian, double price)
    {
        var menuItem = new MenuItem(name, description, vegetarian, price);
        _menuItems.Add(menuItem);
    }

    public IEnumerable<MenuItem> GetMenuItems() => _menuItems;
}

public class Waitress
{
    private readonly IMenu _pancakeHouseMenu;
    private readonly IMenu _dinerMenu;
    private readonly IMenu _cafeMenu;

    public Waitress(IMenu pancakeHouseMenu, IMenu dinerMenu, IMenu cafeMenu)
    {
        _pancakeHouseMenu = pancakeHouseMenu;
        _dinerMenu = dinerMenu;
        _cafeMenu = cafeMenu;
    }

    public void PrintMenu()
    {
        Console.WriteLine("\nMENU\n----\nBREAKFAST");
        PrintMenu(_pancakeHouseMenu.GetMenuItems());
        
        Console.WriteLine("\nLUNCH");
        PrintMenu(_dinerMenu.GetMenuItems());
        
        Console.WriteLine("\nDINNER");
        PrintMenu(_cafeMenu.GetMenuItems());
    }

    public void PrintVegetarianMenu()
    {
        Console.WriteLine("\nVEGETARIAN MENU\n---------------");
        PrintVegetarianItems(_pancakeHouseMenu.GetMenuItems());
        PrintVegetarianItems(_dinerMenu.GetMenuItems());
        PrintVegetarianItems(_cafeMenu.GetMenuItems());
    }

    private void PrintMenu(IEnumerable<MenuItem> menuItems)
    {
        foreach (var menuItem in menuItems)
        {
            Console.WriteLine($"{menuItem.Name}, {(menuItem.IsVegetarian ? "(v)" : "")} -- {menuItem.Price}");
            Console.WriteLine($"     -- {menuItem.Description}");
        }
    }

    private void PrintVegetarianItems(IEnumerable<MenuItem> menuItems)
    {
        foreach (var menuItem in menuItems.Where(item => item.IsVegetarian))
        {
            Console.WriteLine($"{menuItem.Name} -- {menuItem.Price}");
            Console.WriteLine($"     -- {menuItem.Description}");
        }
    }
}



