using System.Text;

namespace command;

internal class CommandPattern
{
    public static void CommandModel()
    {
        RemoteControlWithUndo remote = new RemoteControlWithUndo();

        Light livingRoomLight = new Light("Living Room");

        remote.setCommand(0, new LightOnCommand(livingRoomLight), new LightOffCommand(livingRoomLight));

        remote.onButtonWasPushed(0);
        remote.offButtonWasPushed(0);
        Console.WriteLine(remote.toString());
        remote.undoButtonWasPushed();
        remote.offButtonWasPushed(0);
        remote.onButtonWasPushed(0);
        Console.WriteLine(remote.toString());
        remote.undoButtonWasPushed();

        CeilingFan ceilingFan = new CeilingFan("Living Room");

        remote.setCommand(1, new CeilingFanMediumCommand(ceilingFan), new CeilingFanOffCommand(ceilingFan));
        remote.setCommand(2, new CeilingFanHighCommand(ceilingFan), new CeilingFanOffCommand(ceilingFan));

        remote.onButtonWasPushed(1);
        remote.offButtonWasPushed(1);
        Console.WriteLine(remote.toString());
        remote.undoButtonWasPushed();

        remote.onButtonWasPushed(2);
        Console.WriteLine(remote.toString());
        remote.undoButtonWasPushed();
    }
}

public interface ICommand
{
    public void execute();
    public void undo();
}

public class NoCommand : ICommand
{
    public void execute() => Console.WriteLine("No Command");
    public void undo() => Console.WriteLine("No Command");
}

public class Light(string _name)
{
    public string name = _name;
    public void On() => Console.WriteLine("Light On");
    public void Off() => Console.WriteLine("Light Off");
}

public class LightOnCommand : ICommand
{
    Light light;

    public LightOnCommand(Light light)
    {
        this.light = light;
    }

    public void execute() => light.On();

    public void undo() => light.Off();
}

public class LightOffCommand : ICommand
{
    Light light;

    public LightOffCommand(Light light)
    {
        this.light = light;
    }

    public void execute() => light.Off();

    public void undo() => light.On();
}

public class RemoteControlWithUndo
{
    ICommand[] onCommands;
    ICommand[] offCommands;
    ICommand undoCommand;

    public RemoteControlWithUndo()
    {
        onCommands = new ICommand[7];
        offCommands = new ICommand[7];
        
        var noCommand = new NoCommand();
        for (int i = 0; i < onCommands.Length; i++) 
        {
            onCommands[i] = noCommand;
            offCommands[i] = noCommand;
        }
        undoCommand = noCommand;
    }

    public void setCommand(int slot, ICommand onCommand, ICommand offCommand)
    {
        onCommands[slot] = onCommand;
        offCommands[slot] = offCommand;
    }

    public void onButtonWasPushed(int slot)
    {
        onCommands[slot].execute();
        undoCommand = onCommands[slot];
    }

    public void offButtonWasPushed(int slot)
    {
        offCommands[slot].execute();
        undoCommand = offCommands[slot];
    }

    public void undoButtonWasPushed() 
    { 
        undoCommand.undo();
    }

    public string toString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("\n----Remote Control-----\n");
        for (int i = 0; i < onCommands.Length; i++)
        {
            stringBuilder.Append($"[slot {i}] {onCommands[i].GetType().Name} {offCommands[i].GetType().Name} \n");
        }
        return stringBuilder.ToString();
    }
}

public class CeilingFan(string location)
{
    public static readonly int HIGH = 3;
    public static readonly int MEDIUM = 2;
    public static readonly int LOW = 1;
    public static readonly int OFF = 0;
    public string location = location;
    int speed = OFF;

    public void high()
    {
        Console.WriteLine($"{location} ceiling fan is on high");
        speed = HIGH;
    }
    public void medium() 
    {
        Console.WriteLine($"{location} ceiling fan is on medium");
        speed = MEDIUM;
    }
    public void low()
    {
        Console.WriteLine($"{location} ceiling fan is on low");
        speed = LOW;
    }
    public void off()
    {
        Console.WriteLine($"{location} ceiling fan is off");
        speed = OFF;
    }

    public int getSpeed() => speed;
}

public class CeilingFanHighCommand(CeilingFan ceilingFan) :ICommand
{
    CeilingFan ceilingFan = ceilingFan;
    int prevSpeed;

    public void execute()
    {
        prevSpeed = ceilingFan.getSpeed();
        ceilingFan.high();
    }
    
    public void undo()
    {
        if (prevSpeed == CeilingFan.HIGH) ceilingFan.high();
        else if (prevSpeed == CeilingFan.MEDIUM) ceilingFan.medium();
        else if (prevSpeed == CeilingFan.LOW) ceilingFan.low();
        else ceilingFan.off();
    }
}

public class CeilingFanMediumCommand(CeilingFan ceilingFan) : ICommand
{
    CeilingFan ceilingFan = ceilingFan;
    int prevSpeed;

    public void execute()
    {
        prevSpeed = ceilingFan.getSpeed();
        ceilingFan.medium();
    }

    public void undo()
    {
        if (prevSpeed == CeilingFan.HIGH) ceilingFan.high();
        else if (prevSpeed == CeilingFan.MEDIUM) ceilingFan.medium();
        else if (prevSpeed == CeilingFan.LOW) ceilingFan.low();
        else ceilingFan.off();
    }
}

public class CeilingFanLowCommand(CeilingFan ceilingFan) : ICommand
{
    CeilingFan ceilingFan = ceilingFan;
    int prevSpeed;

    public void execute()
    {
        prevSpeed = ceilingFan.getSpeed();
        ceilingFan.low();
    }

    public void undo()
    {
        if (prevSpeed == CeilingFan.HIGH) ceilingFan.high();
        else if (prevSpeed == CeilingFan.MEDIUM) ceilingFan.medium();
        else if (prevSpeed == CeilingFan.LOW) ceilingFan.low();
        else ceilingFan.off();
    }
}

public class CeilingFanOffCommand(CeilingFan ceilingFan) : ICommand
{
    CeilingFan ceilingFan = ceilingFan;
    int prevSpeed;

    public void execute()
    {
        prevSpeed = ceilingFan.getSpeed();
        ceilingFan.off();
    }

    public void undo()
    {
        if (prevSpeed == CeilingFan.HIGH) ceilingFan.high();
        else if (prevSpeed == CeilingFan.MEDIUM) ceilingFan.medium();
        else if (prevSpeed == CeilingFan.LOW) ceilingFan.low();
        else ceilingFan.off();
    }
}