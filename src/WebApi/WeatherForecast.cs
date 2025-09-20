/// <summary>
/// Representa el pronóstico del clima para el endpoint de ejemplo.
/// </summary>
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    /// <summary>
    /// Temperatura en Fahrenheit calculada a partir de Celsius.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
