namespace WeatherClient.Models;

public record struct WeatherData(int Temperature,
                                          int Precipitation,
                                          int Humidity,
                                          int Wind,
                                          WeatherType Condition);
