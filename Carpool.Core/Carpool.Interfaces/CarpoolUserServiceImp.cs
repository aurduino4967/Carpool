using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.Core.ServiceModels;

namespace Carpool.Core.Interfaces
{
    public  interface CarpoolUserServiceImp
    {
        public bool IsAuthorized();
        public string Signup(User NewUser);
        public string Login(User ExistingUser);
        public string OfferRide(OfferedRide NewRide);
        public string BookRide(BookedRide BookRide);
        public List<OfferedRide> OfferedRides();
        public List<OfferedRide> BookedRides();
        public List<OfferedRide> AvailableRides(OfferedRide SearchRide);
        
    }
}
