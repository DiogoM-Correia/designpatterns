using proxy;

namespace state;

public interface IGumballMachineState
{
    void InsertQuarter();
    void EjectQuarter();
    void TurnCrank();
    void Dispense();
}

public class NoQuarterState : IGumballMachineState
{
    private IGumballMachineRemote _gumballMachine;

    public NoQuarterState(IGumballMachineRemote gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("You inserted a quarter");
        _gumballMachine.SetState(_gumballMachine.GetHasQuarterState());
    }

    public void EjectQuarter()
    {
        Console.WriteLine("You haven't inserted a quarter");
    }

    public void TurnCrank()
    {
        Console.WriteLine("You turned but there's no quarter");
    }

    public void Dispense()
    {
        Console.WriteLine("You need to pay first");
    }
}

public class HasQuarterState : IGumballMachineState
{
    private IGumballMachineRemote _gumballMachine;
    private Random _randomWinner = new Random();

    public HasQuarterState(IGumballMachineRemote gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("You can't insert another quarter");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("Quarter returned");
        _gumballMachine.SetState(_gumballMachine.GetNoQuarterState());
    }

    public void TurnCrank()
    {
        Console.WriteLine("You turned...");
        int winner = _randomWinner.Next(10);
        if (winner == 0 && _gumballMachine.GetCount() > 1)
        {
            _gumballMachine.SetState(_gumballMachine.GetWinnerState());
        }
        else
        {
            _gumballMachine.SetState(_gumballMachine.GetSoldState());
        }
    }

    public void Dispense()
    {
        Console.WriteLine("No gumball dispensed");
    }
}

public class SoldState : IGumballMachineState
{
    private IGumballMachineRemote _gumballMachine;

    public SoldState(IGumballMachineRemote gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("Please wait, we're already giving you a gumball");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("Sorry, you already turned the crank");
    }

    public void TurnCrank()
    {
        Console.WriteLine("Turning twice doesn't get you another gumball!");
    }

    public void Dispense()
    {
        _gumballMachine.ReleaseBall();
        if (_gumballMachine.GetCount() > 0)
        {
            _gumballMachine.SetState(_gumballMachine.GetNoQuarterState());
        }
        else
        {
            Console.WriteLine("Oops, out of gumballs!");
            _gumballMachine.SetState(_gumballMachine.GetSoldOutState());
        }
    }
}

public class SoldOutState : IGumballMachineState
{
    private IGumballMachineRemote _gumballMachine;

    public SoldOutState(IGumballMachineRemote gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("You can't insert a quarter, the machine is sold out");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("You can't eject, you haven't inserted a quarter yet");
    }

    public void TurnCrank()
    {
        Console.WriteLine("You turned, but there are no gumballs");
    }

    public void Dispense()
    {
        Console.WriteLine("No gumball dispensed");
    }
}

public class WinnerState : IGumballMachineState
{
    private IGumballMachineRemote _gumballMachine;

    public WinnerState(IGumballMachineRemote gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("Please wait, we're already giving you a gumball");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("Sorry, you already turned the crank");
    }

    public void TurnCrank()
    {
        Console.WriteLine("Turning twice doesn't get you another gumball!");
    }

    public void Dispense()
    {
        Console.WriteLine("YOU'RE A WINNER! You get two gumballs for your quarter");
        _gumballMachine.ReleaseBall();
        if (_gumballMachine.GetCount() == 0)
        {
            _gumballMachine.SetState(_gumballMachine.GetSoldOutState());
        }
        else
        {
            _gumballMachine.ReleaseBall();
            if (_gumballMachine.GetCount() > 0)
            {
                _gumballMachine.SetState(_gumballMachine.GetNoQuarterState());
            }
            else
            {
                Console.WriteLine("Oops, out of gumballs!");
                _gumballMachine.SetState(_gumballMachine.GetSoldOutState());
            }
        }
    }
}

public class StatePattern
{
    public static void StateModel()
    {
        GumballMachine gumballMachine = new GumballMachine("Test Location", 5);

        Console.WriteLine("\n--- State Pattern Demo ---\n");

        // First customer
        gumballMachine.InsertQuarter();
        gumballMachine.TurnCrank();

        Console.WriteLine("\nGumballs remaining: " + gumballMachine.GetCount());
        Console.WriteLine("\n-----------------");

        // Second customer
        gumballMachine.InsertQuarter();
        gumballMachine.EjectQuarter();
        gumballMachine.TurnCrank();

        Console.WriteLine("\nGumballs remaining: " + gumballMachine.GetCount());
        Console.WriteLine("\n-----------------");

        // Third customer
        gumballMachine.InsertQuarter();
        gumballMachine.TurnCrank();
        gumballMachine.InsertQuarter();
        gumballMachine.TurnCrank();
        gumballMachine.EjectQuarter();

        Console.WriteLine("\nGumballs remaining: " + gumballMachine.GetCount());
        Console.WriteLine("\n-----------------");
    }
}
