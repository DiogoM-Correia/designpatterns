namespace patterns;

internal class Observer()
{
    public static void ObserverModel()
    {
        WeatherData weatherData = new WeatherData();

        CurrentConditionDisplay currentConditionDisplay = new CurrentConditionDisplay(weatherData);
        HeatIndexDisplay heatIndexDisplay = new HeatIndexDisplay(weatherData);
        TemperatureResumeDisplay temperatureResumeDisplay = new TemperatureResumeDisplay(weatherData);

        weatherData.setMeasurements(80, 65, 30.4f);
        weatherData.setMeasurements(82, 70, 29.2f);
        weatherData.setMeasurements(78, 90, 29.2f);
    }
}

public interface IObserver
{
    public void update();
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

    private void notifyObservers()
    {
        observers.ForEach(o => o.update());
    }

    private void measurementsChanged()
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

    public float getTemperature()
    { 
        return temperature; 
    }

    public float getHumidity()
    {
        return humidity;
    }

    public float getPressure()
    {
        return pressure;
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

    public void update()
    {
        this.temperature = this.weatherData.getTemperature();
        this.humidity = this.weatherData.getHumidity();
        display();
    }

    public void display()
    {
        Console.WriteLine($"Current conditions: {temperature} is F degrees and {humidity} % humidity");
    }
}   

public class HeatIndexDisplay : IObserver, IDisplayElement
{
    private float heatIndex;
    private readonly WeatherData weatherData;

    public HeatIndexDisplay(WeatherData weatherData)
    {
        this.weatherData = weatherData;
        weatherData.registerObserver(this);
    }

    public void update()
    {
        float temperature = this.weatherData.getTemperature();
        float humidity = this.weatherData.getHumidity();
        this.heatIndex = computeHeatIndex(temperature, humidity);
        display();
    }

    private float computeHeatIndex(float t, float rh)
    {
        float index = (float)((16.923 + (0.185212 * t) + (5.37941 * rh) - (0.100254 * t * rh) +
            (0.00941695 * (t * t)) + (0.00728898 * (rh * rh)) +
            (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) +
            (0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 *
            (rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) +
            (0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) +
            0.000000000843296 * (t * t * rh * rh * rh)) -
            (0.0000000000481975 * (t * t * t * rh * rh * rh)));
        return index;
    }

    public void display()
    {
        Console.WriteLine($"Heat index is {heatIndex}");
    }
}

public class TemperatureResumeDisplay: IObserver, IDisplayElement
{
    IList<float> temperatures = new List<float>();
    private readonly WeatherData weatherData;

    public TemperatureResumeDisplay(WeatherData weatherData)
    {
        this.weatherData = weatherData;
        weatherData.registerObserver(this);
    }

    public void update()
    {
        temperatures.Add(this.weatherData.getTemperature());
        display();
    }

    public void display()
    {
        Console.WriteLine($"Avg/Max/Min temperatures: {temperatures.Average()}/{temperatures.Max()}/{temperatures.Min()}");
    }

}
