public abstract class Duck
{
    protected FlyBehaviour flyBehaviour = new FlyWithWings(); 
    protected QuackBehaviour quackBehaviour = new DoQuack();
    public Duck() { }

    public abstract void display();

    public void PerformFly()
    {
        flyBehaviour.Fly();
    }

    public void SetFlyBehaviour(FlyBehaviour fb)
    { 
        flyBehaviour = fb; 
    }

    public void PerformQuack()
    {
        quackBehaviour.Quack();
    }

    public void SetQuackBehaviour(QuackBehaviour qb)
    {
        quackBehaviour = qb;
    }

    public void swim ()
    {
        Console.WriteLine("All ducks float, even decoys");
    }
}

public class MallardDucK : Duck
{
    public MallardDucK()
    {
    }

    public override void display()
    {
        Console.WriteLine("I'm a real Mallard DucK");
    }
}

public interface FlyBehaviour
{
    public void Fly();
}

public class FlyWithWings : FlyBehaviour
{
    public void Fly() { Console.WriteLine("I do fly!"); }
}

public class FlyNoWay : FlyBehaviour
{
    public void Fly() { Console.WriteLine("Shit no, I don't fly!"); }
}

public interface QuackBehaviour
{
    public void Quack();
}

public class DoQuack : QuackBehaviour
{
    public void Quack() { Console.WriteLine("Quack!"); }
}

public class MuteQuack : QuackBehaviour
{
    public void Quack() { Console.WriteLine("<<Silence>>"); }
}

public class Squeak : QuackBehaviour
{
    public void Quack() { Console.WriteLine("Squeak!"); }
}

class Program
{
    static void Main(string[] args)
    {
        var duck = new MallardDucK();
        duck.PerformQuack();
        duck.PerformFly();
        duck.display();

        var model = new ModelDuck();
        model.PerformFly();
        model.SetFlyBehaviour(new FlyRocketPowered());
        model.PerformFly();
    }
}

public class ModelDuck : Duck
{
    public ModelDuck()
    {
        flyBehaviour = new FlyNoWay();
        quackBehaviour = new DoQuack();
    }

    public override void display() { Console.WriteLine("I'm a model duck"); }
}

public class FlyRocketPowered : FlyBehaviour
{
    public void Fly() { Console.WriteLine("I'm flying with a rocket"); }
}