namespace templatemethod;

internal class TemplateMethod()
{
    public static void TemplateMethodModel()
    {
        TeaWithHook tea = new();
        CoffeeWithHook coffee = new();

        Console.WriteLine("Making tea...");
        tea.prepareRecipe();

        Console.WriteLine("Making coffee...");
        coffee.prepareRecipe();
    }
}

public abstract class CaffeineBeverageWithHook()
{
    public void prepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        if (CustomerWantsCondiments())
        {
            AddCondiments();
        }
    }

    public abstract void Brew();

    public abstract void AddCondiments();

    void BoilWater()
    {
        Console.WriteLine("Boiling water");
    }

    void PourInCup()
    {
        Console.WriteLine("Pouring into cup");
    }

    bool CustomerWantsCondiments() 
    {
        return true;
    }
}

public class CoffeeWithHook : CaffeineBeverageWithHook
{
    public override void Brew()
    {
        Console.WriteLine("Dripping Coffee through filter");
    }

    public override void AddCondiments()
    {
        Console.WriteLine("Adding Sugar and Milk");
    }

    public bool CustomerWantsCondiments()
    {
        var answer = GetUserInput();

        if (answer.ToLower().StartsWith("y"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string GetUserInput()
    {
        string? answer = null;

        Console.WriteLine("Would you like milk and sugar with your coffee (y/n)?");

        return "yes";
    }
}

public class TeaWithHook : CaffeineBeverageWithHook
{
    public override void Brew()
    {
        Console.WriteLine("Dripping tea leaves through filter");
    }

    public override void AddCondiments()
    {
        Console.WriteLine("Adding Sugar and Milk");
    }

    public bool CustomerWantsCondiments()
    {
        var answer = GetUserInput();

        if (answer.ToLower().StartsWith("y"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string GetUserInput()
    {
        string? answer = null;

        Console.WriteLine("Would you like milk and sugar with your coffee (y/n)?");

        return "yes";
    }
}