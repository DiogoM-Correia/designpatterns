namespace adapter;

internal class AdapterPattern
{
    public static void AdapterModel()
    {
        Duck duck = new MallardDuck();

        Turkey turkey = new WildTurkey();
        Duck turkeyAdapter = new TurkeyAdapter(turkey);

        Console.WriteLine("The turkey says...");
        turkey.gobble();
        turkey.fly();

        Console.WriteLine("The Duck says...");
        testDuck(duck);

        Console.WriteLine("The TurkeyAdapter says...");
        testDuck(turkeyAdapter);
    }

    static void testDuck(Duck duck)
    {
        duck.quack();
        duck.fly();
    }
}

public interface Duck
{
    public void quack();
    public void fly();
}

public class MallardDuck : Duck
{
    public void quack() { Console.WriteLine("Quack"); }

    public void fly() { Console.WriteLine("I'm flying"); }
}

public interface Turkey
{
    public void gobble();
    public void fly();
}

public class WildTurkey : Turkey
{
    public void gobble() { Console.WriteLine("Gobble gobble"); }
    public void fly() { Console.WriteLine("Flying, but little"); }
}

public class TurkeyAdapter : Duck
{
    Turkey turkey;

    public TurkeyAdapter(Turkey turkey)
    {
        this.turkey = turkey;
    }

    public void quack() { turkey.gobble(); }
    public void fly() 
    {
        for (int i = 0; i < 5; i++)
        {
            turkey.fly();
        }
    }
}

public class DuckAdapter : Turkey
{
    Duck duck;

    public DuckAdapter(Duck duck)
    {
        this.duck = duck;
    }

    public void gobble() { duck.quack(); }
    public void fly() { duck.fly(); }
}