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
    private GumballMachine _gumballMachine;

    public NoQuarterState(GumballMachine gumballMachine)
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
    private GumballMachine _gumballMachine;
    private Random _randomWinner = new Random();

    public HasQuarterState(GumballMachine gumballMachine)
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
    private GumballMachine _gumballMachine;

    public SoldState(GumballMachine gumballMachine)
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
    private GumballMachine _gumballMachine;

    public SoldOutState(GumballMachine gumballMachine)
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

public class WinnerState(GumballMachine gumballMachine) : IGumballMachineState
{
    private GumballMachine _gumballMachine = gumballMachine;

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

public class GumballMachine
{
    private readonly IGumballMachineState _soldOutState;
    private readonly IGumballMachineState _noQuarterState;
    private readonly IGumballMachineState _hasQuarterState;
    private readonly IGumballMachineState _soldState;
    private readonly IGumballMachineState _winnerState;

    private IGumballMachineState _state;
    private int _count = 0;

    public GumballMachine(int numberGumballs)
    {
        _soldOutState = new SoldOutState(this);
        _noQuarterState = new NoQuarterState(this);
        _hasQuarterState = new HasQuarterState(this);
        _soldState = new SoldState(this);
        _winnerState = new WinnerState(this);

        _count = numberGumballs;
        if (numberGumballs > 0)
        {
            _state = _noQuarterState;
        }
        else
        {
            _state = _soldOutState;
        }
    }

    public void InsertQuarter() => _state.InsertQuarter();
    public void EjectQuarter() => _state.EjectQuarter();
    public void TurnCrank()
    {
        _state.TurnCrank();
        _state.Dispense();
    }

    public void SetState(IGumballMachineState state)
    {
        _state = state;
    }

    public void ReleaseBall()
    {
        Console.WriteLine("A gumball comes rolling out the slot...");
        if (_count > 0)
        {
            _count--;
        }
    }

    public int GetCount() => _count;
    public IGumballMachineState GetState() => _state;
    public IGumballMachineState GetSoldOutState() => _soldOutState;
    public IGumballMachineState GetNoQuarterState() => _noQuarterState;
    public IGumballMachineState GetHasQuarterState() => _hasQuarterState;
    public IGumballMachineState GetSoldState() => _soldState;
    public IGumballMachineState GetWinnerState() => _winnerState;
}

public class StatePattern
{
    public static void StateModel()
    {
        GumballMachine gumballMachine = new GumballMachine(5);

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
