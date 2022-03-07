using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.Models;

namespace Carpool.Interfaces
{
    public  interface CarpoolServiceInterface
    {
        public string Signup(string email, string password);
        public string Login(string email, string password);
        public string OfferRide(DateTime date, string time, string from, string to, string stops,float price, int seats);
        public string BookRide(string offerid, int seats);
        public List<OfferedRide> OfferedRides();
        public List<OfferedRide> BookedRides();
        public List<OfferedRide> AvailableRides(DateTime date, string time, string fromplace, string toplace, int seats);
    }
}
