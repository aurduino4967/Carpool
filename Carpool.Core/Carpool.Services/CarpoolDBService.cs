using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.Core.ServiceModels;
using Carpool.Core.Interfaces;

namespace Carpool.Core.Services
{
    public class CarpoolDBService:CarpoolDBImp
    {
        private readonly CarpoolDBContext Context;
        public CarpoolDBService()
        {
             Context = new CarpoolDBContext();
        }
        public string CreateNewUser(User NewUser)
        {
            try
            {
                Context.Users.Add(NewUser);
                Context.SaveChanges();
                return "done succcessfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<OfferedRide> GetOfferedRides()
        {
            List<OfferedRide> rides=new List<OfferedRide>();
            foreach(OfferedRide ride in Context.OfferedRides)
                rides.Add(ride);
            return rides;
        }

        public User? GetUser(string email, string password)
        {
                User? CurrentUser = Context.Users.SingleOrDefault(o => o.Uname.TrimEnd() == email && o.Password.TrimEnd() == password);
                if (CurrentUser != null)
                    return CurrentUser;
                else
                    return null;
           
 
        }
        public string CreateNewOfferedRide(OfferedRide Ride)
        {
            try
            {
                Context.OfferedRides.Add(Ride);
                Context.SaveChanges(true);
                return "done successfully";
            }
            catch(Exception e)
            { return e.Message; }
        }
        public void  CreateNewBookedRide(BookedRide Ride)
        {
          
                Context.BookedRides.Add(Ride);
                Context.SaveChanges();
          
            
        }
        public void UpdateOfferedRide(OfferedRide Ride)
        {
            Context.OfferedRides.Update(Ride);
            Context.SaveChanges();
        }

        public OfferedRide? GetOfferedRideById(string OfferId)
        {
            return Context.OfferedRides.SingleOrDefault(o => o.OfferId == OfferId);

        }
        public List<BookedRide> GetBookedRides()
        {
            List<BookedRide> rides = new List<BookedRide>();
            foreach (BookedRide ride in Context.BookedRides)
                rides.Add(ride);
            return rides;

        }
    }
}
