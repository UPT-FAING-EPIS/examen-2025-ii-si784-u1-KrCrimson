using System;
using System.Collections.Generic;

namespace Infrastructure.DbScaffold;

public partial class Flight
{
    public Guid Id { get; set; }

    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string Airline { get; set; } = null!;

    public decimal Price { get; set; }

    public int AvailableSeats { get; set; }

    public int TotalSeats { get; set; }
}
