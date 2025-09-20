using System;
using System.Collections.Generic;

namespace Infrastructure.DbScaffold;

public partial class Reservation
{
    public Guid Id { get; set; }

    public Guid FlightId { get; set; }

    public Guid UserId { get; set; }

    public int SeatNumber { get; set; }

    public DateTime ReservationDate { get; set; }

    public bool IsCancelled { get; set; }
}
