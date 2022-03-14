using System;
using System.Collections.Generic;

namespace Carpool.Core.ServiceModels
{ 

    public partial class User
    {
        public User()
        {
            BookedRides = new HashSet<BookedRide>();
            OfferedRides = new HashSet<OfferedRide>();
        }

        public string Uname { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<BookedRide> BookedRides { get; set; }
        public virtual ICollection<OfferedRide> OfferedRides { get; set; }
    }
}
