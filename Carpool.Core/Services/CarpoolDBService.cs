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
        public string CreateNewUser(User newUser)
        {
            try
            {
                Context.Users.Add(newUser);
                Context.SaveChanges();
                return "Account Created Successfully";
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

        public User? GetUser(User existingUser)
        {
                User? CurrentUser = Context.Users.SingleOrDefault(o => o.Uname.TrimEnd() == existingUser.Uname && o.Password.TrimEnd() == existingUser.Password);
                if (CurrentUser != null)
                    return CurrentUser;
                else
                    return null;
           
 
        }
        public string CreateNewOfferedRide(OfferedRide ride)
        {
            try
            {
                Context.OfferedRides.Add(ride);
                Context.SaveChanges(true);
                return "Ride Registered Successfully";
            }
            catch(Exception e)
            { return e.Message; }
        }
        public void  CreateNewBookedRide(BookedRide ride)
        {
          
                Context.BookedRides.Add(ride);
                Context.SaveChanges();
          
            
        }
        public void UpdateOfferedRide(OfferedRide ride)
        {
            Context.OfferedRides.Update(ride);
            Context.SaveChanges();
        }

        public OfferedRide? GetOfferedRideById(string offerId)
        {
            return Context.OfferedRides.SingleOrDefault(o => o.OfferId == offerId);

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
