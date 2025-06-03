using System;
using System.Collections.Generic;

namespace CabBookingSystem.API.ScaffoldModels;

public partial class Ride
{
    public int RideId { get; set; }

    public int UserId { get; set; }

    public int DriverId { get; set; }

    public string PickupLocation { get; set; } = null!;

    public string DropoffLocation { get; set; } = null!;

    public decimal Fare { get; set; }

    public string? Status { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual User User { get; set; } = null!;
}
