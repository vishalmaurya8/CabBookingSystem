using System;
using System.Collections.Generic;

namespace CabBookingSystem.API.ScaffoldModels;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int RideId { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Ride Ride { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
