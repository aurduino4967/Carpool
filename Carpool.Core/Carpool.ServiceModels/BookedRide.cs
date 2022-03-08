using System;
using System.Collections.Generic;

namespace Carpool.Core.ServiceModels
{
    public partial class BookedRide
    {
        public string OfferId { get; set; } = null!;
        public string BookedBy { get; set; } = null!;
        public int Seats { get; set; }
        public double Price { get; set; }

        public virtual User BookedByNavigation { get; set; } = null!;
        public virtual OfferedRide Offer { get; set; } = null!;
    }
}
