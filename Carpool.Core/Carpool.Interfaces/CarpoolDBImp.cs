using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.Core.ServiceModels;

namespace Carpool.Core.Interfaces
{
    public interface CarpoolDBImp
    {
        public string CreateNewUser(User NewUser);
        public List<OfferedRide> GetOfferedRides();
        public User? GetUser(string email, string password);
        public string CreateNewOfferedRide(OfferedRide Ride);
        public void CreateNewBookedRide(BookedRide Ride);
        public void UpdateOfferedRide(OfferedRide Ride);
        public OfferedRide? GetOfferedRideById(string OfferId);
        public List<BookedRide> GetBookedRides();

    }
}
