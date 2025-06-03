using System;
using System.Collections.Generic;

namespace CabBookingSystem.API.ScaffoldModels;

public partial class Rating
{
    public int RatingId { get; set; }

    public int RideId { get; set; }

    public int FromUserId { get; set; }

    public int ToUserId { get; set; }

    public int Score { get; set; }

    public string? Comments { get; set; }

    public virtual User FromUser { get; set; } = null!;

    public virtual Ride Ride { get; set; } = null!;

    public virtual User ToUser { get; set; } = null!;
}
