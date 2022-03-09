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
        private readonly CarpoolDBImp DBObject;
        private string Email=string.Empty;
        public CarpoolUserService(CarpoolDBImp DBObj)
        {
            DBObject = DBObj;
        }

        public bool IsAuthorized()
        {
            return !string.IsNullOrEmpty(Email);
        }
     
        public string Signup(User NewUser)
        {
            return DBObject.CreateNewUser(NewUser);
        }

        public List<OfferedRide> AvailableRides(OfferedRide SearchRide)
        {
            List<OfferedRide> MatchedRides = new();
            bool flag;
            string[] stops;
            try
            {
                //iterating over each ride for a match
                foreach (OfferedRide Ride in DBObject.GetOfferedRides())
                {
                    flag = false;   //a flag which tells that the ride has perfectly matched search ride initially it is false
                    //if the ride matches all the requirements except toplace
                    if (Ride.Date == SearchRide.Date && Ride.Time.TrimEnd() == SearchRide.Time && Ride.FromPlace.TrimEnd() == SearchRide.FromPlace.ToLower() && Ride.Seats >= SearchRide.Seats)
                    {
                        if (Ride.Stops != null && Ride.Stops.Contains(','))
                        {
                            stops = Ride.Stops.Split(',');

                            //if the matched raid contains multiple stops
                            foreach (string stop in stops)
                            {
                                if (stop.TrimEnd().Equals(SearchRide.ToPlace) || Ride.ToPlace.Equals(SearchRide.ToPlace))
                                {
                                    flag = true;
                                }

                            }
                        }
                        //if the matched raid has a single stop
                        else if ((Ride.Stops != null && Ride.Stops.TrimEnd().Equals(SearchRide.ToPlace)) || Ride.ToPlace.Equals(SearchRide.ToPlace))
                            flag = true;

                        
                    }
                    if (flag)
                        MatchedRides.Add(Ride);

                }
                return MatchedRides;
            }
            catch
            { return new List<OfferedRide>();}
        }

       


        public string Login(User ExistingUser)
        {
            try
            {
                User? CurrentUser = DBObject.GetUser(ExistingUser);
                if (CurrentUser != null)
                {
                    Email = CurrentUser.Uname.TrimEnd();
                    return "done successfully" ;
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

        public string OfferRide(OfferedRide NewRide)
        {
            if (IsAuthorized())
            {
                NewRide.OfferedBy = Email;
                NewRide.OfferId = NewRide.OfferedBy + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss");

                return DBObject.CreateNewOfferedRide(NewRide);
            }
            else
            { return "do login"; }    

        }


        public string BookRide(BookedRide NewRideBook)
        {
            if (IsAuthorized())
            {
                try
                {
                    OfferedRide? Ride = DBObject.GetOfferedRideById(NewRideBook.OfferId);
                    if (Ride != null)
                    {

                        NewRideBook.BookedBy = Email;
                        NewRideBook.Price = Ride.Price * NewRideBook.Seats;
                        Ride.Seats -= NewRideBook.Seats;
                        DBObject.CreateNewBookedRide(NewRideBook);
                        DBObject.UpdateOfferedRide(Ride);
                        return "Ride Booked Successfully";
                    }
                    else
                    { return "invalid offerid"; }

                }
                catch (Exception e)
                { return e.Message; }
            }
            else
            { return "Do LogIn "; }

        }



        public List<OfferedRide> OfferedRides()
        {

            List<OfferedRide> res = new List<OfferedRide>();

            foreach (OfferedRide Ride in DBObject.GetOfferedRides())
            {
                if (Ride.OfferedBy.TrimEnd() == Email)
                {

                    res.Add(Ride);
                }

            }
            return res;

        }


        public List<OfferedRide> BookedRides()
        {

            List<OfferedRide> res = new();
            List<BookedRide> Bookings=new List<BookedRide>();   

            foreach (BookedRide booked in DBObject.GetBookedRides())
            {
                if (booked.BookedBy.TrimEnd() == Email)
                {
                    Bookings.Add(booked);
                }
            }
            foreach(BookedRide Booked in Bookings)
                    res.Add(item: DBObject.GetOfferedRideById(Booked.OfferId));
            return res;

        }
        public string GetEmail()
        {
            return Email;
        }
    }
}
