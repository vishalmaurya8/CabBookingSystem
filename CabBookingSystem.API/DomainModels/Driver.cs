using System;
using System.Collections.Generic;

namespace CabBookingSystem.API.ScaffoldModels;

public partial class Driver
{
    public int DriverId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string LicenseNumber { get; set; } = null!;

    public string VehicleDetails { get; set; } = null!;

    public string? Status { get; set; }

    public virtual ICollection<Ride> Rides { get; set; } = new List<Ride>();
}
