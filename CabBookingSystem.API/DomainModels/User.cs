using System;
using System.Collections.Generic;

namespace CabBookingSystem.API.ScaffoldModels;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Rating> RatingFromUsers { get; set; } = new List<Rating>();

    public virtual ICollection<Rating> RatingToUsers { get; set; } = new List<Rating>();

    public virtual ICollection<Ride> Rides { get; set; } = new List<Ride>();
}
