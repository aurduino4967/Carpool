using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.Core.ServiceModels;
using Carpool.Core.Interfaces;
namespace Carpool.Core.Services
{
    public  class CarpoolUserService: CarpoolUserServiceImp
    {
        private static CarpoolUserService instance=new CarpoolUserService();
        private readonly CarpoolDBImp DBObject=new CarpoolDBService();
        private User? CurrentUser;
        private CarpoolUserService() { }
        public static CarpoolUserService Instance
        {
            get { return instance; }
        }

        public bool IsAuthorized()
        { 
            return CurrentUser !=null;
        }

        public string Signup(User newUser)
        {
            return DBObject.CreateNewUser(newUser);
        }

        public List<OfferedRide> AvailableRides(OfferedRide searchRide)
        {
            List<OfferedRide> matchedRides = new();
            bool flag;
            string[] stops;
            try
            {
                //iterating over each ride for a match
                foreach (OfferedRide Ride in DBObject.GetOfferedRides())
                {
                    flag = false;   //a flag which tells that the ride has perfectly matched search ride initially it is false
                    //if the ride matches all the requirements except toplace
                    if (Ride.Date == searchRide.Date && Ride.Time.TrimEnd() == searchRide.Time && Ride.FromPlace.TrimEnd() == searchRide.FromPlace.ToLower() && Ride.Seats >= searchRide.Seats)
                    {
                        if (Ride.Stops != null && Ride.Stops.Contains(','))
                        {
                            stops = Ride.Stops.Split(',');
                            //if the matched raid contains multiple stops
                            foreach (string stop in stops)
                            {
                                if (stop.TrimEnd().Equals(searchRide.ToPlace) || Ride.ToPlace.Equals(searchRide.ToPlace))
                                {
                                    flag = true;
                                }

                            }
                        }
                        //if the matched raid has a single stop
                        else if ((Ride.Stops != null && Ride.Stops.TrimEnd().Equals(searchRide.ToPlace)) || Ride.ToPlace.Equals(searchRide.ToPlace))
                            flag = true;
                    }
                    if (flag)
                        matchedRides.Add(Ride);    
                }
                return matchedRides;
            }
            catch
            { 
                return new List<OfferedRide>();
            }
        }
        
        public string Login(User existingUser)
        {
            try
            {
                CurrentUser= DBObject.GetUser(existingUser);
                if (CurrentUser !=null)
                {
                    return "Successfully Loged In" ;
                }
                else
                {
                    return "invalid login credentials";
                }
            }
            catch (Exception ex)    
            {
                return ex.Message;
            }
        }

        public string OfferRide(OfferedRide newRide)
        {
            if (IsAuthorized())
            {
                newRide.OfferedBy = CurrentUser.Uname;
                newRide.OfferId = string.Concat(newRide.OfferedBy, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));

                return DBObject.CreateNewOfferedRide(newRide);
            }
            else
            {
                return "Haven't logged in? Login"; 
            }    

        }


        public string BookRide(BookedRide newRideBook)
        {
            if (IsAuthorized())
            {
                try
                {
                    OfferedRide? ride = DBObject.GetOfferedRideById(newRideBook.OfferId);
                    if (ride != null)
                    {

                        newRideBook.BookedBy = CurrentUser.Uname;
                        newRideBook.Price = ride.Price * newRideBook.Seats;
                        ride.Seats -= newRideBook.Seats;
                        DBObject.CreateNewBookedRide(newRideBook);
                        DBObject.UpdateOfferedRide(ride);
                        return "Ride Booked Successfully";
                    }
                    else
                    { return "invalid offerid"; }

                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            else
            { 
                return "Haven't loged in? login "; 
            }

        }



        public List<OfferedRide> OfferedRides()
        {

            List<OfferedRide> res = new List<OfferedRide>();
            if (IsAuthorized())
            {
                foreach (OfferedRide Ride in DBObject.GetOfferedRides())
                {
                    if (Ride.OfferedBy.TrimEnd() == CurrentUser.Uname)
                    {

                        res.Add(Ride);
                    }

                }
            }
            return res;

        }


        public List<OfferedRide> BookedRides()
        {
            List<OfferedRide> bookedRides = new();
            List<BookedRide> bookings = new List<BookedRide>();
            if (IsAuthorized())
            {
            

                foreach (BookedRide booked in DBObject.GetBookedRides())
                {
                    if (booked.BookedBy.TrimEnd() == CurrentUser.Uname)
                    {
                        bookings.Add(booked);
                    }
                }
                foreach (BookedRide Booked in bookings)
                    bookedRides.Add(item: DBObject.GetOfferedRideById(Booked.OfferId));
            }
            return bookedRides;

        }
        
    }
}
