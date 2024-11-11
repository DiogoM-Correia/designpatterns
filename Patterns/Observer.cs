namespace patterns;

internal class Observer()
{
    public static void ObserverModel()
    {
        WeatherData weatherData = new WeatherData();

        CurrentConditionDisplay currentConditionDisplay = new CurrentConditionDisplay(weatherData);

        weatherData.setMeasurements(80, 65, 30.4f);
        weatherData.setMeasurements(82, 70, 29.2f);
        weatherData.setMeasurements(78, 90, 29.2f);
    }

}

public interface IObserver
{
    public void update(float temperature, float humidity, float pressure);
}

public interface IDisplayElement
{
    public void display();
}

public interface ISubject
{
    public void registerObserver(IObserver o);
    public void removeObserver(IObserver o);
    public void notifyObservers();
}

public class WeatherData : ISubject
{
    private List<IObserver> observers;
    private float temperature;
    private float humidity;
    private float pressure;

    public WeatherData()
    {
        observers = new List<IObserver>();
    }

    public void registerObserver(IObserver o)
    {
        observers.Add(o);
    }

    public void removeObserver(IObserver o)
    {
        observers.Remove(o);
    }

    public void notifyObservers()
    {
        observers.ForEach(o => o.update(temperature, humidity, pressure));
    }

    public void measurementsChanged()
    {
        notifyObservers();
    }

    public void setMeasurements(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        measurementsChanged();
    }
}

public class CurrentConditionDisplay : IObserver, IDisplayElement
{
    private float temperature;
    private float humidity;
    private readonly WeatherData weatherData;

    public CurrentConditionDisplay(WeatherData weatherData)
    {
        this.weatherData = weatherData;
        weatherData.registerObserver(this);
    }

    public void update(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        display();
    }

    public void display()
    {
        Console.WriteLine($"Current conditions: {temperature} is F degrees and {humidity} % humidity");
    }
}   
