using System;
using System.Collections.Generic;

namespace Carpool.Models
{
    public partial class OfferedRide
    {
        public string OfferId { get; set; } = null!;
        public string OfferedBy { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Time { get; set; } = null!;
        public string FromPlace { get; set; } = null!;
        public string ToPlace { get; set; } = null!;
        public int Seats { get; set; }
        public double Price { get; set; }
        public string? Stops { get; set; }

        public virtual User OfferedByNavigation { get; set; } = null!;
        public virtual BookedRide BookedRide { get; set; } = null!;
    }
}
