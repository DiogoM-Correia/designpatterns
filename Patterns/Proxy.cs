using System;
using state;

namespace proxy;

// Remote interface
public interface IGumballMachineRemote
{
    int GetCount();
    string GetLocation();
    IGumballMachineState GetState();
    void SetState(IGumballMachineState state);
    void ReleaseBall();
    IGumballMachineState GetSoldOutState();
    IGumballMachineState GetNoQuarterState();
    IGumballMachineState GetHasQuarterState();
    IGumballMachineState GetSoldState();
    IGumballMachineState GetWinnerState();
}

// Real Subject - Modified GumballMachine to implement remote interface
public class GumballMachine : IGumballMachineRemote
{
    private readonly IGumballMachineState _soldOutState;
    private readonly IGumballMachineState _noQuarterState;
    private readonly IGumballMachineState _hasQuarterState;
    private readonly IGumballMachineState _soldState;
    private readonly IGumballMachineState _winnerState;
    private readonly string _location;

    private IGumballMachineState _state;
    private int _count = 0;

    public GumballMachine(string location, int numberGumballs)
    {
        _soldOutState = new SoldOutState(this);
        _noQuarterState = new NoQuarterState(this);
        _hasQuarterState = new HasQuarterState(this);
        _soldState = new SoldState(this);
        _winnerState = new WinnerState(this);
        _location = location;

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

    // Interface implementation
    public int GetCount() => _count;
    public string GetLocation() => _location;
    public IGumballMachineState GetState() => _state;

    // State getters
    public IGumballMachineState GetSoldOutState() => _soldOutState;
    public IGumballMachineState GetNoQuarterState() => _noQuarterState;
    public IGumballMachineState GetHasQuarterState() => _hasQuarterState;
    public IGumballMachineState GetSoldState() => _soldState;
    public IGumballMachineState GetWinnerState() => _winnerState;
}

// Proxy
public class GumballProxy(IGumballMachineRemote machine)
{
    private readonly IGumballMachineRemote _machine = machine;

    public void Report()
    {
        Console.WriteLine("Gumball Machine: " + _machine.GetLocation());
        Console.WriteLine("Current inventory: " + _machine.GetCount() + " gumballs");
        Console.WriteLine("Current state: " + _machine.GetState());
        Console.WriteLine();
    }
}

// Demo code for the Proxy Pattern
public class ProxyPattern
{
    public static void ProxyModel()
    {
        Console.WriteLine("\n--- Proxy Pattern Demo ---\n");

        // Create gumball machines at different locations
        GumballMachine machine1 = new("New York", 5);
        GumballMachine machine2 = new ("Chicago", 2);
        GumballMachine machine3 = new ("Los Angeles", 8);

        // Create monitors for each machine
        GumballProxy monitor1 = new (machine1);
        GumballProxy monitor2 = new (machine2);
        GumballProxy monitor3 = new (machine3);

        // Initial reports
        Console.WriteLine("=== Initial Reports ===");
        monitor1.Report();
        monitor2.Report();
        monitor3.Report();

        // Simulate some activity
        Console.WriteLine("=== After Some Activity ===");
        machine1.InsertQuarter();
        machine1.TurnCrank();
        machine2.InsertQuarter();
        machine2.TurnCrank();
        machine2.InsertQuarter();
        machine2.TurnCrank();

        // Updated reports
        monitor1.Report();
        monitor2.Report();
        monitor3.Report();
    }
} 